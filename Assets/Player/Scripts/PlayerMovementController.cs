using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MovementController
{
    [SerializeField] private float baseMovementSpeed = 2f;
    private float movementSpeedMultiplier = 1;

    private InputAction moveAction;

    public event Action<float> MovementSpeedMultiplierChanged;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (moveAction.IsPressed())
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>().normalized;
            base.Move(moveValue * Time.deltaTime * baseMovementSpeed * movementSpeedMultiplier);
        }
    }

    public void IncrementMovementSpeedMultiplier(float increase)
    {
        increase = Mathf.Max(0, increase);
        SetMovementSpeedMultiplier(movementSpeedMultiplier + increase);
    }

    public void SetMovementSpeedMultiplier(float multiplier)
    {
        float prev = movementSpeedMultiplier;
        movementSpeedMultiplier = Mathf.Max(0, multiplier);

        if (Mathf.Approximately(prev, movementSpeedMultiplier)) return;

        MovementSpeedMultiplierChanged?.Invoke(movementSpeedMultiplier);
    }
}
