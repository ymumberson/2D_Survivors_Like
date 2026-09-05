using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStats weaponStats;
    [SerializeField] protected List<string> targetTags = new();
    protected const float DELAY_BETWEEN_PROJECTILES = 0.1f;
    private Vector3 previousPosition;
    private Vector3 movementDirection = Vector3.one;
    protected Character _character;
    protected AttackController _attackController;
    protected OnHitController _onHitController;

    protected float Damage => _attackController.Damage * weaponStats.DamageAmount;
    protected float WeaponSpeed => weaponStats.WeaponSpeed * _attackController.ProjectileSpeed;
    protected float WeaponSize => weaponStats.WeaponSize * _attackController.ProjectileSize;
    protected float AttackInterval => weaponStats.AttackInterval / _attackController.AttackSpeed;
    protected Vector3 MovementDirection => movementDirection;
    
    public virtual void Initialize(Character character)
    {
        _character = character;
        _attackController = character.AttackController;
        _onHitController = character.OnHitController;
        gameObject.SetActive(true);
    }

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

    void Update()
    {
        CalculateMovementDirection();
        previousPosition = transform.position;
    }

    private bool IsStationary()
    {
        return (
            Mathf.Approximately(previousPosition.x, transform.position.x) &&
            Mathf.Approximately(previousPosition.y, transform.position.y) &&
            Mathf.Approximately(previousPosition.z, transform.position.z)
        );
    }

    private void CalculateMovementDirection()
    {
        if (previousPosition == null)
        {
            movementDirection = Vector3.one;
        }
        else
        {
            Vector3 newMoveDirection = (transform.position - previousPosition).normalized;
            if (newMoveDirection.magnitude > 0)
            {
                movementDirection = newMoveDirection;
            }
        }
    }

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

    public void IncreaseStats(WeaponStats statsIncrease)
    {
        //TODO
    }

    public void DecreaseStats(WeaponStats statsDecrease)
    {
        //TODO
    }

    [System.Serializable]
    public struct WeaponStats
    {
        public float DamageAmount;
        public float WeaponSpeed;
        public float WeaponSize;
        public float AttackInterval;

        public WeaponStats(
            float damageAmount,
            float weaponSpeed,
            float weaponSize,
            float attackInterval
        )
        {
            DamageAmount = damageAmount;
            WeaponSpeed = weaponSpeed;
            WeaponSize = weaponSize;
            AttackInterval = attackInterval;
        }
    }
}
