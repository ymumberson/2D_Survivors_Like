using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected AttackController attackController;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileSize = 1f;
    [SerializeField] protected List<string> targetTags = new();

    protected float Damage => attackController.Damage * damageAmount;
    protected float AttackInterval => attackInterval / attackController.AttackSpeed;
    protected float ProjectileSpeed => projectileSpeed * attackController.ProjectileSpeed;
    protected float ProjectileSize => projectileSize * attackController.ProjectileSize;
}
