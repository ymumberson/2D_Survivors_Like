using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTMP;
    [SerializeField] private Slider experienceBar;
    [SerializeField] private Slider healthBar;

    private HealthController _healthController;
    private ExperienceController _experienceController;
    private bool hasRunStart = false;

    void OnEnable()
    {
        if (!hasRunStart) return;

        _healthController.HealthChanged += HandleHealthChanged;
        _experienceController.ExperienceChanged += HandleExperienceChanged;
    }

    void Start()
    {
        Initialise();
        hasRunStart = true;
        OnEnable();
    }

    void OnDisable()
    {
        _healthController.HealthChanged -= HandleHealthChanged;
        _experienceController.ExperienceChanged -= HandleExperienceChanged;
    }

    private void Initialise()
    {
        Player player = GameController.Instance.GetPlayer();
        _healthController = player.GetComponentInChildren<HealthController>();
        _experienceController = player.GetComponentInChildren<ExperienceController>();
    }

    private void HandleExperienceChanged(float newExperienceValue)
    {
        float percentage = newExperienceValue / _experienceController.LevelUpCost;
        experienceBar.value = percentage;
    }

    private void HandleHealthChanged(float newHealthValue)
    {
        float percentage = newHealthValue / _healthController.MaxHealth;
        healthBar.value = percentage;
    }
}
