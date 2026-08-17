using System;
using Unity.Mathematics;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    private const float LEVEL_UP_COST_MULTIPLIER = 2f;
    private const float BASE_LEVEL_UP_COST = 10;
    [SerializeField] private float experienceGainMultiplier = 1;
    [SerializeField] private int startingLevel = 1;
    private int _level;
    public int Level => _level;
    private float _experience;
    public float Experience => _experience;
    private float _levelUpCost;
    public float LevelUpCost => _levelUpCost;

    public event Action<int> LevelledUp;
    public event Action<float> GainedExperience;
    public event Action<float> ExperienceChanged;

    void Awake()
    {
        _experience = 0;
        _level = startingLevel;
        _levelUpCost = CalculateLevelUpCost(_level);
    }

    public void AddExperience(float experienceGain)
    {
        SetExperience(_experience + experienceGain * experienceGainMultiplier);
    }

    private void SetExperience(float experience)
    {
        float previousExperience = _experience;
        _experience = Math.Max(0, experience);

        // Exit if experience gain was none
        if (Mathf.Approximately(previousExperience, _experience)) return;

        // Fire event for experience gained
        float delta = _experience - previousExperience;
        GainedExperience?.Invoke(delta);

        // Level up if we have enough experience
        if (_experience >= _levelUpCost)
        {
            LevelUp();
        }

        ExperienceChanged?.Invoke(_experience);
    }

    private void LevelUp()
    {
        if (_experience < _levelUpCost) return;

        // Loop to account for if we are levelling up multiple times at once
        while (_experience >= _levelUpCost)
        {
            // Level up, reset experience, and calclate next level up cost
            _level++;
            _experience -= _levelUpCost;
            _levelUpCost = CalculateLevelUpCost();

            // Fire event for level up. Will fire multiple times if levelling up multiple times.
            LevelledUp?.Invoke(_level);
        }
    }

    public float CalculateLevelUpCost()
    {
        return CalculateLevelUpCost(_level + 1);
    }

    public float CalculateLevelUpCost(int level)
    {
        /**
            At current values this will produce levels roughly:
            level 1: 12xp
            level 2: 18xp
            level 3: 28xp
            level 4: 42xp
            ...
            level 30: 1810xp
        **/
        return Mathf.RoundToInt(BASE_LEVEL_UP_COST + level * level * LEVEL_UP_COST_MULTIPLIER);
    }
}
