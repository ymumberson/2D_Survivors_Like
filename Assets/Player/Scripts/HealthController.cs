using System;
using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField][Min(0)] private float maxHealth = 100f;
    [SerializeField][Min(0)] private float regenerationPerSecond = 0f;
    [SerializeField][Min(0)] private float damageCooldown = 0.15f;
    private bool canTakeDamage = true;
    private Coroutine healthRegenerationCoroutine;
    public float HealthRegeneration => regenerationPerSecond;
    public float Health => _health;
    public float MaxHealth => maxHealth;
    public bool IsDead
    {
        get;
        private set;
    }
    private float _health;

    public event Action<float> MaxHealthChanged;
    public event Action<float> HealthChanged;
    public event Action<float> Damaged;
    public event Action<float> Healed;
    public event Action Died;
    public event Action Revived;

    private void Awake()
    {
        _health = maxHealth;
    }

    void OnEnable()
    {
        StartHealthRegeneration();
    }

    void OnDisable()
    {
        canTakeDamage = true;
    }

    public void IncrementHealthRegeneration(float regenerationIncrease)
    {
        SetHealthRegeneration(regenerationPerSecond + regenerationIncrease);
    }

    public void SetHealthRegeneration(float regenerationPerSecond)
    {
        this.regenerationPerSecond = Mathf.Max(0, regenerationPerSecond);
        StartHealthRegeneration();
    }

    private void StartHealthRegeneration()
    {
        if (regenerationPerSecond <= 0 || healthRegenerationCoroutine != null) return;

        healthRegenerationCoroutine = StartCoroutine(RegenerateHealth());
    }

    private IEnumerator RegenerateHealth()
    {
        var oneSecondDelay = new WaitForSeconds(1);
        while (true)
        {
            yield return oneSecondDelay;
            Heal(regenerationPerSecond);
        }
    }

    public void Damage(float damageAmount)
    {
        if (IsDead || !canTakeDamage) return;

        damageAmount = Mathf.Max(0f, damageAmount);
        SetHealth(_health - damageAmount);

        StartCoroutine(StartDamageCooldownTimer());
    }

    private IEnumerator StartDamageCooldownTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
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

    public void IncreaseMaxHealth(float increase)
    {
        increase = Mathf.Max(0, increase);
        SetMaxHealth(maxHealth + increase);
    }
    
    public void SetMaxHealth(float maxHealth)
    {
        float prev = this.maxHealth;
        this.maxHealth = Mathf.Max(1, maxHealth);

        if (Mathf.Approximately(prev, this.maxHealth)) return;

        MaxHealthChanged?.Invoke(this.maxHealth);
    }
}
