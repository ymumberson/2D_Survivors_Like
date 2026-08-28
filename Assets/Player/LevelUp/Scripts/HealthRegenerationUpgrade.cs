using UnityEngine;

[CreateAssetMenu(fileName = "HealthRegenerationUpgrade", menuName = "Level Up/Health/Health Regeneration")]
public class HealthRegenerationUpgrade : LevelUpItem
{
    [SerializeField, Min(0)] private float amount;
    
    public override void Apply(Player player)
    {
        player.HealthController.IncrementHealthRegeneration(amount);
    }

    public override void Remove(Player player)
    {
        player.HealthController.IncrementHealthRegeneration(-amount);
    }
}
