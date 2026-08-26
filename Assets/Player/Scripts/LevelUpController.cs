using UnityEngine;

[RequireComponent(typeof(ExperienceController))]
public class LevelUpController : MonoBehaviour
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private AttackController attackController;
    
    private ExperienceController _experienceController;

    void Awake()
    {
        _experienceController = GetComponent<ExperienceController>();
    }

    void OnEnable()
    {
        _experienceController.LevelledUp += HandleLevelledUp;
    }

    void OnDisable()
    {
        _experienceController.LevelledUp -= HandleLevelledUp;
    }

    private void HandleLevelledUp(int newLevel)
    {
        int randomValue = Random.Range(1,9);
        switch (randomValue)
        {
            default:
            case 1:
                healthController.IncreaseMaxHealth(10f);
                Debug.Log("Increasing max health!");
                break;
            case 2:
                healthController.IncrementHealthRegeneration(1f);
                Debug.Log("Increasing health regen!");
                break;
            case 3:
                movementController.IncrementMovementSpeedMultiplier(1f);
                Debug.Log("Increasing move speed!");
                break;
            case 4:
                attackController.IncrementDamageMultiplier(0.2f);
                Debug.Log("Increasing damage mult!");
                break;
            case 5:
                attackController.IncrementAttackSpeedMultiplier(0.1f);
                Debug.Log("Increasing attack speed mult!");
                break;
            case 6:
                attackController.IncrementProjectileSpeedMultiplier(0.2f);
                Debug.Log("Increasing projectile speed mult!");
                break;
            case 7:
                attackController.IncrementProjectileSizeMultiplier(0.2f);
                Debug.Log("Increasing projectile size mult!");
                break;
            case 8:
                attackController.IncrementProjectileCount(1);
                Debug.Log("Increasing projectile count!");
                break;
        }
    }
}
