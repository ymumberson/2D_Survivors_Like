using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform rootTransform;
    [SerializeField] private float baseMovementSpeed = 1f;
    [SerializeField] private float movementSpeedMultiplier = 1f;
    private Rigidbody2D _rigidBody;

    public float MovementSpeed => baseMovementSpeed * movementSpeedMultiplier;

    public event Action<float> MovementSpeedMultiplierChanged;

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

    public void IncrementMovementSpeedMultiplier(float increase)
    {
        SetMovementSpeedMultiplier(movementSpeedMultiplier + increase);
    }

    private void SetMovementSpeedMultiplier(float movementSpeedMultiplier)
    {
        float previous = this.movementSpeedMultiplier;
        this.movementSpeedMultiplier = Mathf.Max(0, movementSpeedMultiplier);

        if (Mathf.Approximately(previous, this.movementSpeedMultiplier)) return;

        MovementSpeedMultiplierChanged?.Invoke(this.movementSpeedMultiplier);
    }
}
