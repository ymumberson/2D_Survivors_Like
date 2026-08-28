using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileCountUpgrade", menuName = "Level Up/Attack/Projectile Count")]
public class ProjectileCountUpgrade : LevelUpItem
{
    [SerializeField, Min(0)] private int amount;
    
    public override void Apply(Player player)
    {
        player.AttackController.IncrementProjectileCount(amount);
    }

    public override void Remove(Player player)
    {
        player.AttackController.IncrementProjectileCount(-amount);
    }
}
