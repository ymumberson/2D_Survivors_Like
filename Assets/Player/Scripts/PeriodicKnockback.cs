using System.Collections;
using UnityEngine;

public class PeriodicKnockback : MonoBehaviour
{
    [SerializeField] private float knockbackRadius = 10f;
    [SerializeField] private float knockbackAmount = 2f;
    [SerializeField] private float knockbackInterval = 2f;
    [SerializeField] private LayerMask targetLayer;

    void OnEnable()
    {
        StartCoroutine(KnockbackCycle());
    }

    private IEnumerator KnockbackCycle()
    {
        while (true)
        {
            Collider2D[] withinRadius = Physics2D.OverlapCircleAll(
                transform.position,
                knockbackRadius,
                targetLayer
            );

            foreach (Collider2D collision in withinRadius)
            {
                Character character = collision.gameObject.GetComponent<Character>();
                if (character)
                {
                    Vector2 knockbackDirection = (Vector2)character.transform.position - (Vector2)transform.position;
                    float distance = knockbackDirection.magnitude;
                    knockbackDirection.Normalize();
                    character.MovementController.ApplyKnockback(knockbackDirection, Mathf.Log10(1f+ distance * knockbackAmount * 9f));
                }
                else
                {
                    Debug.LogError("No character");
                }
            }

            yield return new WaitForSeconds(knockbackInterval);
        }
    }
}
