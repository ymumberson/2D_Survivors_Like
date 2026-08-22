using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int spawnCount = 1;
    [SerializeField] private AttackTarget attackTarget = AttackTarget.Closest;
    
    public enum AttackTarget
    {
        Closest=0,
        Random=1,
    }

    void Start()
    {
        StartCoroutine(SpawnProjectiles());
    }

    private IEnumerator SpawnProjectiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        var enemies = GameController.Instance.GetEnemies();
        if (enemies == null || enemies.Count == 0)
            return;

        Vector3 target = SelectTarget(enemies);
        Vector2 targetDirection = (target - transform.position).normalized;

        GameObject projectileGO = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (!projectile)
        {
            Destroy(projectileGO);
            return;
        }
        
        projectile.damage = Damage;
        projectile.direction = targetDirection;
        projectile.speed = ProjectileSpeed;
        projectile.targetTags = targetTags;
        projectile.transform.localScale = projectile.transform.localScale * ProjectileSize;
    }

    private Vector3 SelectTarget(Dictionary<HealthController, GameObject> enemies)
    {
        Transform targetTransform = null;
        switch (attackTarget)
        {
            case AttackTarget.Random:
                int randomIndex = UnityEngine.Random.Range(0, enemies.Count -1);
                targetTransform = enemies[enemies.Keys.ElementAt(randomIndex)].transform;
                break;

            default:
            case AttackTarget.Closest:
                float closestDistance = float.PositiveInfinity;
                foreach (GameObject enemyGO in enemies.Values)
                {
                    float distance = Vector3.Distance(transform.position, enemyGO.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetTransform = enemyGO.transform;
                    }
                }
            break;
        }

        if (targetTransform == null) return Vector3.zero;

        return targetTransform.position;
    }
}
