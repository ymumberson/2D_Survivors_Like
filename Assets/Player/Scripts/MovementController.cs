using System;
using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform rootTransform;
    [SerializeField] private float baseMovementSpeed = 1f;
    [SerializeField] private float movementSpeedMultiplier = 1f;
    private Rigidbody2D _rigidBody;
    private Vector2 movementDirection = Vector2.zero;

    public float MovementSpeed => baseMovementSpeed * movementSpeedMultiplier;

    public Vector2 MovementDirection => movementDirection;

    public event Action<float> MovementSpeedMultiplierChanged;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveAmount)
    {
        if (!rootTransform) return;

        movementDirection = moveAmount.normalized;

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

    public void ApplyKnockback(Vector3 direction, float magnitude)
    {
        StartCoroutine(ApplyKnockbackOverTime(direction, magnitude));
    }

    private IEnumerator ApplyKnockbackOverTime(Vector3 direction, float magnitude)
    {
        if (rootTransform)
        {
            direction.Normalize();

            float interval = 0f;
            float delay = 0.1f;
            float multiplier = magnitude / delay;
            while (interval <= delay)
            {
                if (_rigidBody)
                {
                    _rigidBody.AddForce(direction * magnitude * Time.deltaTime);
                } else
                {
                    rootTransform.position = rootTransform.position + direction * magnitude * Time.deltaTime * multiplier;
                }
                interval += Time.deltaTime;
                yield return null;
            }
        }
    }
}
