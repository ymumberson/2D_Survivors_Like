using UnityEngine;

public class PlayerTestScript : MonoBehaviour
{
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private float healAmount = 2f;
    [SerializeField] private bool revive = false;
    private HealthController _healthController;
    private float elapsed = 0f;

    void Awake()
    {
        _healthController = GetComponentInChildren<HealthController>();
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed > 2f)
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
