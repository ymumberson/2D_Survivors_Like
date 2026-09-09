using UnityEngine;

public readonly struct HitContext
{
    public readonly Character Attacker;
    public readonly Character Target;
    public readonly float Damage;
    public readonly Vector2 Direction;
    public readonly float Knockback;

    public HitContext(
        Character attacker,
        Character target,
        float damage,
        Vector2 direction,
        float knockback
    )
    {
        Attacker = attacker;
        Target = target;
        Damage = damage;
        Direction = direction;
        Knockback = knockback;
    }
}
