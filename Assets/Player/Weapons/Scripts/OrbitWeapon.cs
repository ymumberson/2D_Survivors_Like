using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : Weapon
{
    [SerializeField] private GameObject orbitProjectile;
    [SerializeField] private float orbitDuration = 2f;
    private List<GameObject> orbitProjectiles = new();
    private Vector3 baseScale = Vector3.one;

    void Awake()
    {
        orbitProjectile.SetActive(false);
        baseScale = orbitProjectile.transform.GetChild(0).localScale;
        InstantiateOrbitProjectiles(_attackController.ProjectileCount);
        SetWeaponsActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _attackController.ProjectileCountChanged += InstantiateOrbitProjectiles;
        _attackController.ProjectileSizeMultiplierChanged += HandleWeaponSizeChanged;
    }

    void OnDisable()
    {
        _attackController.ProjectileCountChanged -= InstantiateOrbitProjectiles;
        _attackController.ProjectileSizeMultiplierChanged -= HandleWeaponSizeChanged;
        SetWeaponsActive(false);
    }

    private void InstantiateOrbitProjectiles(int projectileCount)
    {
        while (orbitProjectiles.Count > projectileCount)
        {
            int lastIndex = orbitProjectiles.Count - 1;

            GameObject projectile = orbitProjectiles[lastIndex];

            orbitProjectiles.RemoveAt(lastIndex);
            Destroy(projectile);
        }

        while (orbitProjectiles.Count < projectileCount)
        {
            GameObject projectile =
                Instantiate(orbitProjectile, transform);

            Weapon weapon = projectile.gameObject.GetComponentInChildren<Weapon>(true);
            weapon.Initialize(_character);

            orbitProjectiles.Add(projectile);

            foreach (Transform child in projectile.transform)
            {
                child.localScale *= WeaponSize;
            }
        }
    }

    private void HandleWeaponSizeChanged(float weaponSize)
    {
        foreach (GameObject projectile in orbitProjectiles)
        {
            foreach (Transform child in projectile.transform)
            {
                child.localScale = baseScale * WeaponSize;
            }
        }
    }

    protected override IEnumerator Attack()
    {
        Coroutine firstOrbitProjectile = null;

        float startingRotation = Random.value * 360f;

        GameObject[] orbitProjectilesCopy = orbitProjectiles.ToArray();
        for (int i=0; i<orbitProjectilesCopy.Length; ++i)
        {
            var orbitCoroutine = StartCoroutine(PerformOrbit(orbitProjectilesCopy[i], i, startingRotation));
            firstOrbitProjectile ??= orbitCoroutine;

            yield return new WaitForSeconds(DELAY_BETWEEN_PROJECTILES);
        }

        yield return firstOrbitProjectile;
    }

    private IEnumerator PerformOrbit(GameObject projectile, int projectileIndex, float startingRotation)
    {
        projectile.SetActive(true);

        float elapsed = 0;

        // Here we're making the projectiles evenly spaced, accounting for delays in spawning.
        float offset = (360f / orbitProjectiles.Count) * (projectileIndex); // Ensures projectiles are spread.
        offset += (DELAY_BETWEEN_PROJECTILES * projectileIndex / orbitDuration) * 360f; // Account for delay between projectiles
        
        while (elapsed < orbitDuration)
        {
            elapsed += Time.deltaTime;
            float t = (elapsed / orbitDuration) * _attackController.AttackSpeedMultiplier;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, startingRotation + offset + t * 360f));
            yield return null;
        }

        projectile.SetActive(false);
    }

    private void SetWeaponsActive(bool active)
    {
        foreach (GameObject projectile in orbitProjectiles)
        {
            projectile.SetActive(active);
        }
    }
}
