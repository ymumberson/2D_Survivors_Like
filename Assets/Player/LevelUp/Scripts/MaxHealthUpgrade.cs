using UnityEngine;

[CreateAssetMenu(fileName = "MaxHealthUpgrade", menuName = "Level Up/Health/Max Health")]
public class MaxHealthUpgrade : LevelUpItem
{
    [SerializeField, Min(0)] private float amount;
    
    public override void Apply(Player player)
    {
        player.HealthController.IncreaseMaxHealth(amount);
    }

    public override void Remove(Player player)
    {
        player.HealthController.IncreaseMaxHealth(-amount);
    }
}
