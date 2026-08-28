using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSizeUpgrade", menuName = "Level Up/Attack/Projectile Size")]
public class ProjectileSizeUpgrade : LevelUpItem
{
    [SerializeField, Min(0)] private float amount;
    
    public override void Apply(Player player)
    {
        player.AttackController.IncrementProjectileSizeMultiplier(amount);
    }

    public override void Remove(Player player)
    {
        player.AttackController.IncrementProjectileSizeMultiplier(-amount);
    }
}
