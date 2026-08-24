using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected AttackController attackController;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float weaponSpeed = 10f;
    [SerializeField] private float weaponSize = 1f;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] protected List<string> targetTags = new();

    protected float Damage => attackController.Damage * damageAmount;
    protected float WeaponSpeed => weaponSpeed * attackController.ProjectileSpeed;
    protected float WeaponSize => weaponSize * attackController.ProjectileSize;
    protected float AttackInterval => attackInterval / attackController.AttackSpeed;
    
    void OnEnable()
    {
        StartCoroutine(AttackLoop());
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);
            yield return StartCoroutine(Attack());
        }
    }

    protected abstract IEnumerator Attack();
}
