using UnityEngine;

public abstract class OnHitEffect : ScriptableObject
{
    /// <summary>
    /// Trigger the on-hit effect of this item, including scaling.
    /// </summary>
    /// <param name="hit">Hit context</param>
    /// <param name="stackCount">Amount of this item that the character owns. Used for scaling.</param>
    /// <returns>True if the effect triggered, otherwise false. Does not mean the effect applied.</returns>
    public abstract bool OnHit(HitContext hit, int stackCount);
}
