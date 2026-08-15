using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] private Transform rootTransform;
    private HealthController _healthController;

    void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    void OnEnable()
    {
        _healthController.Died += OnDied;
    }

    void OnDisable()
    {
        _healthController.Died -= OnDied;
    }

    private void OnDied()
    {
        if (rootTransform)
        {
            Destroy(rootTransform.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
