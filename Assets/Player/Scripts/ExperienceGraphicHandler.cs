using System;
using UnityEngine;

public class ExperienceGraphicHandler : MonoBehaviour
{
    [SerializeField] private ExperienceController experienceController;

    void OnEnable()
    {
        experienceController.GainedExperience += HandleGainedExperience;
        experienceController.LevelledUp += HandleLevelledUp;
        experienceController.ExperienceChanged += HandleExperienceChanged;
    }

    void OnDisable()
    {
        experienceController.GainedExperience -= HandleGainedExperience;
        experienceController.LevelledUp -= HandleLevelledUp;
        experienceController.ExperienceChanged -= HandleExperienceChanged;
    }

    private void HandleExperienceChanged(float newExperience)
    {
        Debug.Log($"<color=grey>Experience: {newExperience}</color>");
    }

    private void HandleLevelledUp(int newLevel)
    {
        Debug.Log($"<color=cyan>Level: {newLevel}</color>");
    }

    private void HandleGainedExperience(float experienceGained)
    {
        Debug.Log($"<color=green>Experience: +{experienceGained}</color>");
    }
}
