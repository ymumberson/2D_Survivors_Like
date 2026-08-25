using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private float baseDamage = 1f;
    [SerializeField] private float damageMultiplier = 1f;
    [SerializeField] private float baseAttackSpeed = 1f;
    [SerializeField] private float attackSpeedMultiplier = 1f;
    [SerializeField] private float baseProjectileSpeed = 1f;
    [SerializeField] private float projectileSpeedMultiplier = 1f;
    [SerializeField] private float baseProjectileSize = 1f;
    [SerializeField] private float projectileSizeMultiplier = 1f;
    [SerializeField] private int projectileCount = 1;

    public float DamageMultiplier => damageMultiplier;
    public float AttackSpeedMultiplier => attackSpeedMultiplier;
    public float ProjectileSpeedMultiplier => projectileSpeedMultiplier;
    public float ProjectileSizeMultiplier => projectileSizeMultiplier;

    public float Damage => baseDamage * damageMultiplier;
    public float AttackSpeed => baseAttackSpeed * attackSpeedMultiplier;
    public float ProjectileSpeed => baseProjectileSpeed * projectileSpeedMultiplier;
    public float ProjectileSize => baseProjectileSize * projectileSizeMultiplier;
    public int ProjectileCount => projectileCount;

    public event Action<float> DamageMultiplierChanged;
    public event Action<float> AttackSpeedMultiplierChanged;
    public event Action<float> ProjectileSpeedMultiplierChanged;
    public event Action<float> ProjectileSizeMultiplierChanged;
    public event Action<int> ProjectileCountChanged;

    public void IncrementDamageMultiplier(float increment)
    {
        increment = Mathf.Max(0, increment);
        SetDamageMultiplier(damageMultiplier + increment);
    }
    
    public void SetDamageMultiplier(float damageMultiplier)
    {
        float prev = this.damageMultiplier;
        this.damageMultiplier = Mathf.Max(0, damageMultiplier);

        if (Mathf.Approximately(prev, this.damageMultiplier)) return;

        DamageMultiplierChanged?.Invoke(this.damageMultiplier);
    }

    public void IncrementAttackSpeedMultiplier(float increment)
    {
        increment = Mathf.Max(0, increment);
        SetAttackSpeedMultiplier(attackSpeedMultiplier + increment);
    }

    public void SetAttackSpeedMultiplier(float attackSpeedMultiplier)
    {
        float prev = this.attackSpeedMultiplier;
        this.attackSpeedMultiplier = Mathf.Max(0, attackSpeedMultiplier);

        if (Mathf.Approximately(prev, this.attackSpeedMultiplier)) return;

        AttackSpeedMultiplierChanged?.Invoke(this.attackSpeedMultiplier);
    }

    public void IncrementProjectileSpeedMultiplier(float increment)
    {
        increment = Mathf.Max(0, increment);
        SetProjectileSpeedMultiplier(projectileSpeedMultiplier + increment);
    }

    public void SetProjectileSpeedMultiplier(float projectileSpeedMultiplier)
    {
        float prev = this.projectileSpeedMultiplier;
        this.projectileSpeedMultiplier = Mathf.Max(0, projectileSpeedMultiplier);

        if (Mathf.Approximately(prev, this.projectileSpeedMultiplier)) return;

        ProjectileSpeedMultiplierChanged?.Invoke(this.projectileSpeedMultiplier);
    }

    public void IncrementProjectileSizeMultiplier(float increment)
    {
        increment = Mathf.Max(0, increment);
        SetProjectileSizeMultiplier(projectileSizeMultiplier + increment);
    }

    public void SetProjectileSizeMultiplier(float projectileSizeMultiplier)
    {
        float prev = this.projectileSizeMultiplier;
        this.projectileSizeMultiplier = Mathf.Max(0, projectileSizeMultiplier);

        if (Mathf.Approximately(prev, this.projectileSizeMultiplier)) return;

        ProjectileSizeMultiplierChanged?.Invoke(this.projectileSizeMultiplier);
    }

    public void IncrementProjectileCount(int projectileCountIncrement)
    {
        projectileCountIncrement = Math.Max(0, projectileCountIncrement);
        SetProjectileCount(projectileCount + projectileCountIncrement);
    }

    public void SetProjectileCount(int projectileCount)
    {
        int prevProjectileCount = projectileCount;
        this.projectileCount = Math.Max(0, projectileCount);

        if (prevProjectileCount == this.projectileCount) return;

        ProjectileCountChanged?.Invoke(this.projectileCount);
    }
}
