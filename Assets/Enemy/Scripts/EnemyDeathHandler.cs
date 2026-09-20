using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] private Transform rootTransform;
    [SerializeField] private GameObject experiencePrefab;
    [SerializeField] private float experienceDropAmount = 1;
    [SerializeField] private float experienceDropMultiplier = 1;
    private HealthController _healthController;
    private float experienceRadius = 1f;

    public float ExperienceDropAmount => experienceDropAmount;
    public float ExperienceDropMultiplier => experienceDropMultiplier;

    void Awake()
    {
        _healthController = GetComponent<HealthController>();
        experienceRadius = 
            experiencePrefab.GetComponent<CircleCollider2D>().radius
            * experiencePrefab.transform.localScale.x;
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

    public void SetExperienceDropAmount(float experienceDropAmount)
    {
        this.experienceDropAmount = experienceDropAmount;
    }

    public void SetExperienceDropMultiplier(float experienceDropMultiplier)
    {
        this.experienceDropMultiplier = experienceDropMultiplier;
    }

    private void DropExperience()
    {
        if (!experiencePrefab) return;

        float experienceAmount = experienceDropAmount * experienceDropMultiplier;

        Experience existingExperience = CheckForExistingDroppedExperience(transform.position, experienceRadius);

        if (existingExperience)
        {
            existingExperience.experienceAmount += experienceAmount;
            return;
        }

        GameObject experienceGO = Instantiate(experiencePrefab, transform.position, transform.rotation);
        Experience experience = experienceGO.GetComponent<Experience>();

        if (experience)
            experience.experienceAmount = experienceAmount;
    }

    private Experience CheckForExistingDroppedExperience(Vector3 location, float radius)
    {
        var collisions = Physics2D.OverlapCircleAll(
            location,
            radius
        );

        foreach (var collision in collisions)
        {
            if (collision.TryGetComponent<Experience>(out Experience existingExperience))
            {
                return existingExperience;
            }
        }

        return null;
    }
}
