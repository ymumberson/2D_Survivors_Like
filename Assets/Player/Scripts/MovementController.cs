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
    private Coroutine knockbackCoroutine;

    public float MovementSpeed => baseMovementSpeed * movementSpeedMultiplier;

    public Vector2 MovementDirection => movementDirection;

    public event Action<float> MovementSpeedMultiplierChanged;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveAmount)
    {
        if (!rootTransform || IsBeingKnockedBack()) return;

        movementDirection = moveAmount.normalized;

        ApplyMovement(moveAmount);
    }

    private void ApplyMovement(Vector2 moveAmount)
    {
        SetPosition((Vector2)rootTransform.position + moveAmount);
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
        if (magnitude <= 0) return;

        if (knockbackCoroutine != null)
        {
            StopCoroutine(knockbackCoroutine);
            knockbackCoroutine = null;
        }

        knockbackCoroutine = StartCoroutine(ApplyKnockbackOverTime(direction, magnitude));
    }

    private IEnumerator ApplyKnockbackOverTime(Vector3 direction, float magnitude)
    {
        if (rootTransform)
        {
            direction.Normalize();

            float interval = 0f;
            float delay = 0.1f;
            float force = magnitude / delay;
            while (interval <= delay)
            {
                ApplyMovement(direction * force * Time.deltaTime);
                interval += Time.deltaTime;
                yield return null;
            }
        }

        knockbackCoroutine = null;
    }

    public bool IsBeingKnockedBack()
    {
        return knockbackCoroutine != null;
    }
}
