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
    public event Action<float> Damaged;
    public event Action<float> Healed;
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

        bool previouslyDead = IsDead;
        IsDead = _health <= 0;
        
        HealthChanged?.Invoke(_health);

        if (!previouslyDead && IsDead) // Handle death
        {
            Died?.Invoke();
        } else if (previouslyDead && !IsDead) // Handle revive
        {
            Revived?.Invoke();
        } else if (_health > previousHealth) // Handle healed
        {
            Healed?.Invoke(_health - previousHealth);
        } else if (_health < previousHealth) // Handle damaged
        {
            Damaged?.Invoke(previousHealth - _health);
        }
    }
}
