using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private List<string> targetTags = new();

    private Dictionary<HealthController, Coroutine> toDamage = new();

    void OnDisable()
    {
        foreach (Coroutine coroutine in toDamage.Values)
        {
            StopCoroutine(coroutine);
        }
        toDamage.Clear();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        HealthController healthController = collision.gameObject.GetComponentInChildren<HealthController>();

        // Check if collision is in list of target tags
        if (!healthController || !targetTags.Contains(collision.gameObject.tag)) return;

        if (toDamage.ContainsKey(healthController)) return;

        // Deal initial contact damage, then start DoT
        healthController.Damage(damageAmount);
        toDamage[healthController] = StartCoroutine(DamageOverTime(healthController));
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // NB. This currently assumes one collider for players and enemies
        HealthController healthController = collision.gameObject.GetComponentInChildren<HealthController>();
        
        if (!healthController) return;

        StopDamage(healthController);
    }

    private IEnumerator DamageOverTime(HealthController healthController)
    {
        var waitTimer = new WaitForSeconds(attackInterval);
        while (true)
        {
            yield return waitTimer;

            if (healthController == null || healthController.IsDead)
                break;

            healthController.Damage(damageAmount);
        }

        toDamage.Remove(healthController);
    }

    private void StopDamage(HealthController healthController)
    {
        if (toDamage.TryGetValue(healthController, out Coroutine coroutine))
        {
            StopCoroutine(coroutine);
            toDamage.Remove(healthController);
        }
    }
}
