using System.Collections.Generic;
using UnityEngine;

public class OnHitController : MonoBehaviour
{
    private List<OnHitEffect> onHitEffects = new();

    public void AddOnHitEffect(OnHitEffect onHitEffect)
    {
        onHitEffects.Add(onHitEffect);
    }

    public void RemoveOnHitEffect(OnHitEffect onHitEffect)
    {
        if (onHitEffects.Contains(onHitEffect))
            onHitEffects.Remove(onHitEffect);
    }

    public void OnHit(HitContext hit)
    {
        foreach (OnHitEffect onHitEffect in onHitEffects)
        {
            onHitEffect.OnHit(hit);
        }
    }
}
