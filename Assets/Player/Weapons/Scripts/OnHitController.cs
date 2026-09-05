using System.Collections.Generic;
using UnityEngine;

public class OnHitController : MonoBehaviour
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

    /// <summary>
    /// Trigger a hit. This will attempt to apply all on-hit effects currently owned.
    /// </summary>
    /// <param name="hit">Hit context</param>
    public void OnHit(HitContext hit)
    {
        foreach (var (onHitEffect, stackCount) in onHitEffects)
        {
            onHitEffect.OnHit(hit, stackCount);
        }
    }
}
