using UnityEngine;

[CreateAssetMenu(fileName = "MovementSpeedUpgrade", menuName = "Level Up/Movement/Movement Speed")]
public class MovementSpeedUpgrade : LevelUpItem
{
    [SerializeField, Min(0)] private float amount;
    
    public override void Apply(Player player)
    {
        player.MovementController.IncrementMovementSpeedMultiplier(amount);
    }

    public override void Remove(Player player)
    {
        player.MovementController.IncrementMovementSpeedMultiplier(-amount);
    }
}
