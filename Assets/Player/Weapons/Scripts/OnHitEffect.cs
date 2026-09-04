using UnityEngine;

public abstract class OnHitEffect : ScriptableObject
{
    public abstract void OnHit(HitContext hit);
}
