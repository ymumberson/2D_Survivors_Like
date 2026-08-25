using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingWeapon : Weapon
{
    [SerializeField] private GameObject swingProjectile;
    [SerializeField] private float swingDuration = 0.5f;
    [SerializeField, Range(0,360)] private float swingRadius = 160;
    [SerializeField] private AttackDirection attackDirection = AttackDirection.Closest;
    private List<GameObject> swingProjectiles = new();

    public enum AttackDirection
    {
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3,
        Closest = 4,
        Random = 5,
    }

    void Awake()
    {
        swingProjectile.SetActive(false);
        InstantiateSwingProjectiles(attackController.ProjectileCount);
        SetWeaponsActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        attackController.ProjectileCountChanged += InstantiateSwingProjectiles;
    }

    void OnDisable()
    {
        attackController.ProjectileCountChanged -= InstantiateSwingProjectiles;
        SetWeaponsActive(false);
    }

    private void InstantiateSwingProjectiles(int projectileCount)
    {
        if (swingProjectiles.Count == projectileCount)
        {
            return;
        }
        else if (swingProjectiles.Count > projectileCount)
        {
            swingProjectiles.RemoveRange(projectileCount - 1, swingProjectiles.Count - projectileCount);
            return;
        }
        else
        {
            int numProjectilesToCreate = projectileCount - swingProjectiles.Count;
            for (int i=0; i<numProjectilesToCreate; ++i)
            {
                swingProjectiles.Add(Instantiate(swingProjectile, transform));
            }
        }
    }

    protected override IEnumerator Attack()
    {
        SwingArc swingArc = CalculateSwingArc(attackDirection);

        Coroutine firstSwingProjectile = null;

        foreach (GameObject projectile in swingProjectiles)
        {
            var swingCoroutine = StartCoroutine(SwingProjectile(projectile, swingArc));
            firstSwingProjectile ??= swingCoroutine;

            yield return new WaitForSeconds(DELAY_BETWEEN_PROJECTILES);
        }

        yield return firstSwingProjectile;

        SetWeaponsActive(false);
    }

    private IEnumerator SwingProjectile(GameObject projectile, SwingArc swingArc)
    {
        projectile.SetActive(true);
        float elapsed = 0f;
        while (elapsed <= swingDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, Mathf.Clamp01(elapsed / swingDuration));
            Vector3 rotation = Vector3.Lerp(swingArc.startRotation, swingArc.endRotation, t);
            projectile.transform.localRotation = Quaternion.Euler(rotation);
            yield return null;
        }
        projectile.SetActive(false);
    }

    private void SetWeaponsActive(bool active)
    {
        foreach (GameObject projectile in swingProjectiles)
        {
            projectile.SetActive(active);
        }
    }

    private SwingArc CalculateSwingArc(AttackDirection attackDirection)
    {
        switch (attackDirection)
        {
            case AttackDirection.Up:
                return CalculateSwingArc(transform.position + new Vector3(0,1,0));
            case AttackDirection.Down:
                return CalculateSwingArc(transform.position + new Vector3(0,-1,0));
            case AttackDirection.Left:
                return CalculateSwingArc(transform.position + new Vector3(-1,0,0));
            case AttackDirection.Right:
                return CalculateSwingArc(transform.position + new Vector3(1,0,0));
            case AttackDirection.Random:
                return CalculateSwingArc(transform.position + new Vector3(Random.value-0.5f,Random.value-0.5f,0));
            default:
            case AttackDirection.Closest:
                return CalculateSwingArc(GameController.Instance.GetClosestTarget(transform.position));
        }
    }

    private SwingArc CalculateSwingArc(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float halfArc = swingRadius / 2f;

        float startAngle = targetAngle - halfArc;
        float endAngle = targetAngle + halfArc;

        SwingArc swingArc = new SwingArc
        {
            startRotation = new Vector3(0f, 0f, startAngle),
            endRotation = new Vector3(0f, 0f, endAngle)
        };

        return swingArc;
    }
    
    private struct SwingArc
    {
        public Vector3 startRotation;
        public Vector3 endRotation;
    }
}
