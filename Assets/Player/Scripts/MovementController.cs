using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform rootTransform;
    private Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveAmount)
    {
        if (!rootTransform) return;

        SetPosition(new Vector2(rootTransform.position.x + moveAmount.x, rootTransform.position.y + moveAmount.y));
    }

    public void SetPosition(Vector2 position)
    {
        if (!rootTransform) return;

        if (_rigidBody)
        {
            _rigidBody.MovePosition(position);
        } else
        {
            rootTransform.position = position;
        }
    }
}
