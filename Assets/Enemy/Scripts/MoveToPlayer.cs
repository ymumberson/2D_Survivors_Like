using UnityEngine;

public class MoveToPlayer : MovementController
{
    [SerializeField] private float avoidanceRadius = 1f;
    [SerializeField] private LayerMask avoidanceLayer;
    private const float SLEEP_TIME = 0.25f;
    private const float AVOIDANCE_RADIUS = 0.7071f; // ~45 degrees
    private const float BLOCKED_TIMEOUT_DURATION = 0.1f;
    private float sleepTimer = 0f;
    private bool sleep = false;
    private Player _player;
    private float blockedTimer = 0f;

    private bool moveLeftToAvoid = false;
    
    public void Initialize(Player player)
    {
        _player = player;
        moveLeftToAvoid = Random.Range(0f,1f) > 0.5f ? true : false;
    }

    void FixedUpdate()
    {
        if (sleepTimer > SLEEP_TIME) // Checks if sleep duration is over
        {
            sleep = false;
            sleepTimer = 0;
            return;
        }

        if (sleep) // Continue sleeping
        {
            sleepTimer += Time.fixedDeltaTime;
            return;
        }
        
        Move();
    }

    private void Move()
    {
        if (!_player) return;

        if (!_player.HealthController || _player.HealthController.IsDead) return;

        Vector2 toPlayer = (_player.transform.position - transform.position).normalized;
        Vector2 moveDirection = toPlayer;

        // Check for characters within specified radius that would block path to player
        Collider2D[] withinRadius = Physics2D.OverlapCircleAll(
            transform.position,
            avoidanceRadius,
            avoidanceLayer
        );

        bool blocked = false;
        foreach (Collider2D collision in withinRadius)
        {
            Character character = collision.gameObject.GetComponent<Character>();
            if (character)
            {
                Vector2 toCollision = (Vector2)character.transform.position - (Vector2)transform.position;
                
                // Check if would block path to player, within a radius degree radius
                if (Vector3.Dot(toPlayer.normalized, toCollision.normalized) > AVOIDANCE_RADIUS)
                {
                    blocked = true;
                    break;
                }
            }
            else
            {
                Debug.LogError("No character");
            }
        }

        if (blocked)
        {
            sleep = true;
            blockedTimer += Time.fixedDeltaTime; // Sleep if still blocked after certain time
            if (blockedTimer >= BLOCKED_TIMEOUT_DURATION) return;

            // Set direction to go in to try and avoid the obstacle
            moveDirection = moveLeftToAvoid ? Vector2.Perpendicular(toPlayer) : -Vector2.Perpendicular(toPlayer);

            float rayDistance = MovementSpeed * Time.fixedDeltaTime;

            // Check if direction is free of another obstacle
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                moveDirection,
                rayDistance,
                avoidanceLayer
            );

            if (hit.collider != null) return;
        }
        else
        {
            blockedTimer = 0;
        }
        
        Move(moveDirection * MovementSpeed * Time.fixedDeltaTime);
    }
}
