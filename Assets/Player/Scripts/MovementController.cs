using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform rootTransform;

    public void Move(Vector2 moveAmount)
    {
        if (!rootTransform) return;

        rootTransform.position = new Vector2(rootTransform.position.x + moveAmount.x, rootTransform.position.y + moveAmount.y);
    }

    public void SetPosition(Vector2 position)
    {
        if (!rootTransform) return;

        rootTransform.position = position;
    }
}
