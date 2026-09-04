using UnityEngine;

public abstract class OnHitEffect : LevelUpItem
{
    public override void Apply(Player player)
    {
        player.OnHitController.AddOnHitEffect(this);
    }

    public override void Remove(Player player)
    {
        player.OnHitController.RemoveOnHitEffect(this);
    }

    public abstract void OnHit(GameObject hit);
}
