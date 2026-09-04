using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AttackTarget attackTarget = AttackTarget.Closest;
    [SerializeField] private bool rotateProjectiles = true;
    
    public enum AttackTarget
    {
        Closest=0,
        Random=1,
        MovementDirection=2,
    }

    protected override IEnumerator Attack()
    {
        for (int i=0; i<_attackController.ProjectileCount; ++i)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(DELAY_BETWEEN_PROJECTILES);  
        }
        yield break;
    }

    private void SpawnProjectile()
    {
        var enemies = GameController.Instance.GetEnemies();
        if (enemies == null || enemies.Count == 0)
            return;

        Vector3 target = SelectTarget();
        Vector2 targetDirection = (target - transform.position).normalized;

        GameObject projectileGO = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.Initialise(_onHitController);

        if (!projectile)
        {
            Destroy(projectileGO);
            return;
        }
        
        projectile.damage = Damage;
        projectile.direction = targetDirection;
        projectile.speed = WeaponSpeed;
        projectile.targetTags = targetTags;
        projectile.transform.localScale = projectile.transform.localScale * WeaponSize;

        if (rotateProjectiles)
        {
            projectile.transform.localRotation = CalculateRotation(transform.position, target);
        }
    }

    private Vector3 SelectTarget()
    {
        switch (attackTarget)
        {
            case AttackTarget.Random:
                return GameController.Instance.GetRandomTarget();
            case AttackTarget.MovementDirection:
                return transform.position + MovementDirection;
            default:
            case AttackTarget.Closest:
                return GameController.Instance.GetClosestTarget(transform.position);
        }
    }
}
