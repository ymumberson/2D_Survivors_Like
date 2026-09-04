using UnityEngine;

[CreateAssetMenu(fileName = "TestOnHitItem", menuName = "Level Up/Test/TestOnHitItem")]
public class TestOnHitItem : OnHitEffect
{
    public override void OnHit(GameObject hit)
    {
        hit.transform.parent.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Debug.Log("Hit: " + hit);
    }
}
