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
    private Weapon _weapon;

    public void Initialise(Weapon weapon)
    {
        _weapon = weapon;
    }
    
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

            _weapon.ProcessHit(hc, HitController.HitType.Initial);

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
