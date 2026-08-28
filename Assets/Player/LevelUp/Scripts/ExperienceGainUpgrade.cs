using UnityEngine;

[CreateAssetMenu(fileName = "ExperienceGainUpgrade", menuName = "Level Up/Experience/Experience Gain")]
public class ExperienceGainUpgrade : LevelUpItem
{
    [SerializeField, Min(0)] private float amount;
    
    public override void Apply(Player player)
    {
        player.ExperienceController.IncrementExperienceGainMultiplier(amount);
    }

    public override void Remove(Player player)
    {
        player.ExperienceController.IncrementExperienceGainMultiplier(-amount);
    }
}
