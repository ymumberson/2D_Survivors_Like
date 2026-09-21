using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Experience : MonoBehaviour
{
    public float experienceAmount = 1f;
    public List<string> targetTags = new();
    private CircleCollider2D _collider;

    void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!targetTags.Contains(collision.gameObject.tag)) return;

        ExperienceController xpc = collision.gameObject.GetComponent<ExperienceController>();

        if (!xpc) return;

        xpc.AddExperience(experienceAmount);
        Destroy(gameObject);
    }
}
