using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private AttackController attackController;
    [SerializeField] private ExperienceController experienceController;
    [SerializeField] private MovementController movementController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] WeaponController weaponController;
    [SerializeField] OnHitController onHitController;
    [SerializeField] private List<GameObject> disableOnDied = new();

    public HealthController HealthController => healthController;
    public AttackController AttackController => attackController;
    public ExperienceController ExperienceController => experienceController;
    public MovementController MovementController => movementController;
    public InventoryController InventoryController => inventoryController;
    public WeaponController WeaponController => weaponController;
    public OnHitController OnHitController => onHitController;

    void Awake()
    {
        healthController?.Initialize(this);
        weaponController?.Initialise(this);
    }

    void OnEnable()
    {
        healthController.Died += OnDied;
        healthController.Revived += OnRevived;
    }

    void OnDisable()
    {
        healthController.Died -= OnDied;
        healthController.Revived -= OnRevived;
    }

    private void OnDied()
    {
        foreach (GameObject go in disableOnDied)
        {
            go.SetActive(false);
        }
    }

    private void OnRevived()
    {
        foreach (GameObject go in disableOnDied)
        {
            go.SetActive(true);
        }
    }
}
