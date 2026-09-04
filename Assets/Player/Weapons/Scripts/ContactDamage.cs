using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : Weapon
{
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
        HealthController healthController = collision.gameObject.GetComponent<HealthController>();

        // Check if collision is in list of target tags
        if (!healthController || !targetTags.Contains(collision.gameObject.tag)) return;

        if (toDamage.ContainsKey(healthController)) return;

        // Deal initial contact damage, then start DoT
        healthController.Damage(Damage);
        toDamage[healthController] = StartCoroutine(DamageOverTime(healthController));

        if (_onHitController)
            _onHitController.OnHit(healthController.gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // NB. This currently assumes one collider for players and enemies
        HealthController healthController = collision.gameObject.GetComponent<HealthController>();
        
        if (!healthController) return;

        StopDamage(healthController);
    }

    private IEnumerator DamageOverTime(HealthController healthController)
    {
        var waitTimer = new WaitForSeconds(AttackInterval);
        while (true)
        {
            yield return waitTimer;

            if (healthController == null || healthController.IsDead)
                break;

            healthController.Damage(Damage);

            if (_onHitController)
                _onHitController.OnHit(healthController.gameObject);
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

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
