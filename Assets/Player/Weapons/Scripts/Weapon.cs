using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected AttackController attackController;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float weaponSpeed = 10f;
    [SerializeField] private float weaponSize = 1f;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] protected List<string> targetTags = new();
    protected const float DELAY_BETWEEN_PROJECTILES = 0.1f;

    protected float Damage => attackController.Damage * damageAmount;
    protected float WeaponSpeed => weaponSpeed * attackController.ProjectileSpeed;
    protected float WeaponSize => weaponSize * attackController.ProjectileSize;
    protected float AttackInterval => attackInterval / attackController.AttackSpeed;
    
    protected virtual void OnEnable()
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

    protected Vector3 RotateTo(Vector3 origin, Vector3 target)
    {
        Vector3 direction = target - origin;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return new Vector3(0,0,rotation);
    }

    protected Quaternion CalculateRotation(Vector3 origin, Vector3 target)
    {
        return Quaternion.Euler(RotateTo(origin, target));
    }
}
