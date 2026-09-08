using UnityEngine;

[CreateAssetMenu(fileName = "OnHitLevelUpItem", menuName = "Level Up/On Hit/On Hit Item")]
public class OnHitLevelUpItem : LevelUpItem
{
    [SerializeField] private OnHitEffect onHitEffect;

    public override void Apply(Player player)
    {
        player.HitController.AddOnHitEffect(onHitEffect);
    }

    public override void Remove(Player player)
    {
        player.HitController.RemoveOnHitEffect(onHitEffect);
    }
}
