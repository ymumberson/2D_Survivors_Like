using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction = Vector2.zero;
    public float speed = 1;
    public float damage = 1;
    public List<string> targetTags = new();

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
        if (!targetTags.Contains(collision.gameObject.tag)) return;

        HealthController hc = collision.GetComponentInChildren<HealthController>();

        if (!hc) return;

        hc.Damage(damage);

        Destroy(this.gameObject);
    }
}
