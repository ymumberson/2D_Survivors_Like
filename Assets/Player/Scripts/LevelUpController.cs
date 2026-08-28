using UnityEngine;

[RequireComponent(typeof(ExperienceController))]
public class LevelUpController : MonoBehaviour
{
    [SerializeField] private Player player;
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
        
    }

    private void SelectRandomStatUpgrade()
    {
        int randomValue = Random.Range(1,9);
        switch (randomValue)
        {
            default:
            case 1:
                player.HealthController.IncreaseMaxHealth(10f);
                Debug.Log("Increasing max health!");
                break;
            case 2:
                player.HealthController.IncrementHealthRegeneration(1f);
                Debug.Log("Increasing health regen!");
                break;
            case 3:
                player.MovementController.IncrementMovementSpeedMultiplier(1f);
                Debug.Log("Increasing move speed!");
                break;
            case 4:
                player.AttackController.IncrementDamageMultiplier(0.2f);
                Debug.Log("Increasing damage mult!");
                break;
            case 5:
                player.AttackController.IncrementAttackSpeedMultiplier(0.1f);
                Debug.Log("Increasing attack speed mult!");
                break;
            case 6:
                player.AttackController.IncrementProjectileSpeedMultiplier(0.2f);
                Debug.Log("Increasing projectile speed mult!");
                break;
            case 7:
                player.AttackController.IncrementProjectileSizeMultiplier(0.2f);
                Debug.Log("Increasing projectile size mult!");
                break;
            case 8:
                player.AttackController.IncrementProjectileCount(1);
                Debug.Log("Increasing projectile count!");
                break;
        }
    }
}
