using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction = Vector2.zero;
    public float speed = 1;
    public float damage = 1;
    public List<string> targetTags = new();
    public List<string> obstacleTags = new();
    public bool destroyOnTargetCollision = true;

    void FixedUpdate()
    {
        transform.position = 
        new Vector2(
            transform.position.x + direction.x * speed * Time.deltaTime,
            transform.position.y + direction.y * speed * Time.deltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool destroyedByCollision = false;
        if (targetTags.Contains(collision.gameObject.tag))
        {
            HealthController hc = collision.GetComponentInChildren<HealthController>();

            if (!hc) return;

            hc.Damage(damage);

            if (destroyOnTargetCollision)
            {
                Destroy(gameObject);
                destroyedByCollision = true;
            }  
        }

        if (!destroyedByCollision && obstacleTags.Contains(collision.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
