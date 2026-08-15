using UnityEngine;

public class PlayerTestScript : MonoBehaviour
{
    [SerializeField] private float interval = 2f;
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private float healAmount = 2f;
    [SerializeField] private bool revive = false;
    [SerializeField] private float _currentHealth;
    private HealthController _healthController;
    private float elapsed = 0f;

    void Awake()
    {
        _healthController = GetComponentInChildren<HealthController>();
    }

    void Start()
    {
         _currentHealth = _healthController.Health;
    }

    void OnEnable()
    {
        _healthController.HealthChanged += OnHealthChanged;
    }

    void OnDisable()
    {
        _healthController.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float newHealth)
    {
        _currentHealth = newHealth;
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed > interval)
        {
            elapsed = 0;
            _healthController.Damage(damageAmount);
            _healthController.Heal(healAmount);
        }

        if (revive)
        {
            _healthController.Revive();
            revive = false;
        }
    }
}
