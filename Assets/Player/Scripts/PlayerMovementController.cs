using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MovementController
{
    private InputAction moveAction;

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
            Move(moveValue * Time.deltaTime * MovementSpeed);
        }
    }
}
