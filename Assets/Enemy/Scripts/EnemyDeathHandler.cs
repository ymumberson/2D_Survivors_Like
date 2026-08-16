using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] private Transform rootTransform;
    [SerializeField] private GameObject experiencePrefab;
    [SerializeField] private float experienceDropAmount = 1;
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
        DropExperience();

        if (rootTransform)
        {
            Destroy(rootTransform.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
    }

    private void DropExperience()
    {
        if (!experiencePrefab) return;

        GameObject experienceGO = Instantiate(experiencePrefab, transform.position, transform.rotation);
        Experience experience = experienceGO.GetComponent<Experience>();

        if (experience)
            experience.experienceAmount = experienceDropAmount;
    }
}
