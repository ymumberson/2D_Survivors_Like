using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MovementController
{
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
            base.Move(moveValue * Time.deltaTime * movementSpeed);
        }
    }
}
