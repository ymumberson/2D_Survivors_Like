using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgrade", menuName = "Level Up/Attack/Damage")]
public class DamageUpgrade : LevelUpItem
{
    [SerializeField, Min(0)] private float amount;
    
    public override void Apply(Player player)
    {
        player.AttackController.IncrementDamageMultiplier(amount);
    }

    public override void Remove(Player player)
    {
        player.AttackController.IncrementDamageMultiplier(-amount);
    }
}
