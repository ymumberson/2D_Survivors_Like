using UnityEngine;

[CreateAssetMenu(fileName = "AttackSpeedUpgrade", menuName = "Level Up/Attack/Attack Speed")]
public class AttackSpeedUpgrade : LevelUpItem
{
    [SerializeField, Min(0)] private float amount;
    
    public override void Apply(Player player)
    {
        player.AttackController.IncrementAttackSpeedMultiplier(amount);
    }

    public override void Remove(Player player)
    {
        player.AttackController.IncrementAttackSpeedMultiplier(-amount);
    }
}
