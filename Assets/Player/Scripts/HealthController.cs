using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField][Min(0)] private float maxHealth = 100f;
    public float Health => _health;
    public float MaxHealth => maxHealth;
    public bool IsDead
    {
        get;
        private set;
    }
    private float _health;

    public event Action<float> HealthChanged;
    public event Action Died;
    public event Action Revived;

    private void Awake()
    {
        _health = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        if (IsDead) return; // Cannot damage when dead

        damageAmount = Mathf.Max(0f, damageAmount);
        SetHealth(_health - damageAmount);
    }

    public void Heal(float healAmount)
    {
        if (IsDead) return; // Cannot heal when dead

        healAmount = Mathf.Max(0f, healAmount);
        SetHealth(_health + healAmount);
    }

    public void Revive(float healthAmount = 1f)
    {
        if (!IsDead) return;

        SetHealth(healthAmount);
    }

    private void SetHealth(float health)
    {
        // Clamp health between 0 and max health.
        float previousHealth = _health;
        _health = Mathf.Clamp(health, 0, maxHealth);

        // Return if health has not changed.
        if (Mathf.Approximately(previousHealth, _health)) return;

        HealthChanged?.Invoke(_health);

        if (_health <= 0f && !IsDead) // Handle death
        {
            IsDead = true;
            Died?.Invoke();
        } else if (IsDead && _health > 0) // Handle revive
        {
            IsDead = false;
            Revived?.Invoke();
        }
    }
}
