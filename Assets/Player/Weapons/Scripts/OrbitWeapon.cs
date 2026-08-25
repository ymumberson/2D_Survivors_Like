using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : Weapon
{
    [SerializeField] private GameObject orbitProjectile;
    [SerializeField] private float orbitDuration = 2f;
    private List<GameObject> orbitProjectiles = new();

    void Awake()
    {
        orbitProjectile.SetActive(false);
        InstantiateOrbitProjectiles(attackController.ProjectileCount);
        SetWeaponsActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        attackController.ProjectileCountChanged += InstantiateOrbitProjectiles;
        attackController.ProjectileSizeMultiplierChanged += HandleWeaponSizeChanged;
    }

    void OnDisable()
    {
        attackController.ProjectileCountChanged -= InstantiateOrbitProjectiles;
        attackController.ProjectileSizeMultiplierChanged -= HandleWeaponSizeChanged;
        SetWeaponsActive(false);
    }

    private void InstantiateOrbitProjectiles(int projectileCount)
    {
        if (orbitProjectiles.Count == projectileCount)
        {
            return;
        }
        else if (orbitProjectiles.Count > projectileCount)
        {
            orbitProjectiles.RemoveRange(projectileCount - 1, orbitProjectiles.Count - projectileCount);
            return;
        }
        else
        {
            int numProjectilesToCreate = projectileCount - orbitProjectiles.Count;
            for (int i=0; i<numProjectilesToCreate; ++i)
            {
                orbitProjectiles.Add(Instantiate(orbitProjectile, transform));
                foreach (Transform child in orbitProjectiles[^1].transform)
            {
                child.localScale *= WeaponSize;
            }
            }
        }
    }

    private void HandleWeaponSizeChanged(float weaponSize)
    {
        foreach (GameObject projectile in orbitProjectiles)
        {
            foreach (Transform child in projectile.transform)
            {
                child.localScale = orbitProjectile.transform.localScale * WeaponSize;
            }
        }
    }

    protected override IEnumerator Attack()
    {
        Coroutine firstOrbitProjectile = null;

        float startingRotation = Random.value * 360f;

        for (int i=0; i<orbitProjectiles.Count; ++i)
        {
            var orbitCoroutine = StartCoroutine(PerformOrbit(orbitProjectiles[i], i, startingRotation));
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
            float t = elapsed / orbitDuration;
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
