using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    /// <summary>
    /// All owned on-hit effects and the amount owned of each.
    /// </summary>
    private Dictionary<OnHitEffect, int> onHitEffects = new();

    /// <summary>
    /// Add an on-hit effect to the inventory.
    /// </summary>
    /// <param name="onHitEffect">On-hit effect to add.</param>
    public void AddOnHitEffect(OnHitEffect onHitEffect)
    {
        if (onHitEffects.TryGetValue(onHitEffect, out int stackCount))
        {
            onHitEffects[onHitEffect] = stackCount + 1;
        }
        else
        {
            onHitEffects[onHitEffect] = 1;
        }
    }

    /// <summary>
    /// Remove an on-hit effect from the inventory.
    /// </summary>
    /// <param name="onHitEffect">On-hit effect to remove.</param>
    public void RemoveOnHitEffect(OnHitEffect onHitEffect)
    {
        if (!onHitEffects.TryGetValue(onHitEffect, out int stackCount)) return;

        onHitEffects[onHitEffect] = stackCount - 1;

        if (stackCount <= 1)
        {
            onHitEffects.Remove(onHitEffect);
        }
        else
        {
            onHitEffects[onHitEffect] = stackCount - 1;
        }
    }

    public void ProcessHit(HitContext hit, HitType hitType)
    {
        hit.Target.HealthController.Damage(hit.Damage);
        hit.Target.MovementController.ApplyKnockback(hit.Direction, hit.Knockback);

        if (hitType == HitType.Initial)
            ApplyOnHitEffects(hit);
    }

    /// <summary>
    /// Trigger a hit. This will attempt to apply all on-hit effects currently owned.
    /// </summary>
    /// <param name="hit">Hit context</param>
    private void ApplyOnHitEffects(HitContext hit)
    {
        foreach (var (onHitEffect, stackCount) in onHitEffects)
        {
            onHitEffect.OnHit(hit, stackCount);
        }
    }

    public enum HitType
    {
        Initial=0,
        Repeat=1,
    }
}
