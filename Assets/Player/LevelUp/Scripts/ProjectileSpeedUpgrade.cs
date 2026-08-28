using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSpeedUpgrade", menuName = "Level Up/Attack/Projectile Speed")]
public class ProjectileSpeedUpgrade : LevelUpItem
{
    [SerializeField, Min(0)] private int amount;
    
    public override void Apply(Player player)
    {
        player.AttackController.IncrementProjectileSpeedMultiplier(amount);
    }

    public override void Remove(Player player)
    {
        player.AttackController.IncrementProjectileSpeedMultiplier(-amount);
    }
}
