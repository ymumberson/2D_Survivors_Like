using UnityEngine;

[CreateAssetMenu(fileName = "TestOnHitEffect", menuName = "On Hit Effects/Test/TestOnHitEffect")]
public class TestOnHitEffect : OnHitEffect
{
    public override void OnHit(HitContext hit)
    {
        hit.Target.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
