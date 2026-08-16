using UnityEngine;

public class PlayerTestScript : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float interval = 2f;
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private float healAmount = 2f;
    [SerializeField] private bool revive = false;
    [SerializeField] private float _currentHealth;

    [Header("Experience")]
    [SerializeField] private float experienceGain = 2f;

    // Internal
    private HealthController _healthController;
    private ExperienceController _experienceController;
    private float elapsed = 0f;

    void Awake()
    {
        _healthController = GetComponentInChildren<HealthController>();
        _experienceController = GetComponentInChildren<ExperienceController>();
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
            _experienceController.AddExperience(experienceGain);
        }

        if (revive)
        {
            _healthController.Revive();
            revive = false;
        }
    }
}
