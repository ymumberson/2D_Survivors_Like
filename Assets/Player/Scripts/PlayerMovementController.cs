using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float movementSpeed = 2f;

    private InputAction moveAction;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (moveAction.IsPressed())
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>().normalized;
            HandleMovement(moveValue * Time.deltaTime * movementSpeed);
        }
    }

    private void HandleMovement(Vector2 moveValue)
    {
        if (playerTransform)
        {
            playerTransform.position = new Vector2(playerTransform.position.x + moveValue.x, playerTransform.position.y + moveValue.y);
        }
    }
}
