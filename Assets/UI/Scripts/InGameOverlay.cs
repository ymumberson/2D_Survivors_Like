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
    private int previousSeconds = -1;
    private bool isInitialized = false;
    private bool _isSubscribed = false;

    public void Initialise(Player player)
    {
        _healthController = player.HealthController;
        _experienceController = player.ExperienceController;
        
        isInitialized = true;

        TrySubscribe();
    }
    
    void OnEnable()
    {
        TrySubscribe();
    }

    void OnDisable()
    {
        Unsubscribe();
    }

    private void TrySubscribe()
    {
        if (!isInitialized || _isSubscribed || !isActiveAndEnabled) return;

        _healthController.HealthChanged += HandleHealthChanged;
        _healthController.MaxHealthChanged += HandleMaxHealthChanged;
        _experienceController.ExperienceChanged += HandleExperienceChanged;
        
        _isSubscribed = true;
    }

    private void Unsubscribe()
    {
        if (!_isSubscribed) return;
        
        _healthController.HealthChanged -= HandleHealthChanged;
        _healthController.MaxHealthChanged -= HandleMaxHealthChanged;
        _experienceController.ExperienceChanged -= HandleExperienceChanged;

        _isSubscribed = false;
    }

    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        int seconds = Mathf.FloorToInt(GameController.Instance.ElapsedTime);

        if (seconds == previousSeconds) return;

        previousSeconds = seconds;
        timerTMP.text = FormatTime(seconds);
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

    private void HandleMaxHealthChanged(float newMaxHealth)
    {
        float percentage = _healthController.Health / newMaxHealth;
        healthBar.value = percentage;
    }

    private string FormatTime(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return time.ToString(@"mm\:ss");
    }
}
