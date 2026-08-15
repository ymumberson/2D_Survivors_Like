using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform rootTransform;
    [SerializeField] Bounds movementBounds = new();

    public void Move(Vector2 moveAmount)
    {
        if (!rootTransform) return;

        SetPosition(new Vector2(rootTransform.position.x + moveAmount.x, rootTransform.position.y + moveAmount.y));
    }

    public void SetPosition(Vector2 position)
    {
        if (!rootTransform) return;

        float clampedX = Math.Clamp(position.x, movementBounds.min.x, movementBounds.max.x);
        float clampedY = Math.Clamp(position.y, movementBounds.min.y, movementBounds.max.y);

        rootTransform.position = new Vector3(clampedX, clampedY, 0);
    }
}
