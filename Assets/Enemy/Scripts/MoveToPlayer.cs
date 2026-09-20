using UnityEngine;

public class MoveToPlayer : MovementController
{
    [SerializeField] private float avoidanceRadius = 1f;
    [SerializeField] private LayerMask avoidanceLayer;
    private static float SLEEP_TIME = 0.25f;
    private float sleepTimer = 0f;
    private bool sleep = false;
    private Player _player;
    private float blockedTimer = 0f;

    private bool moveLeftToAvoid = false;
    
    // Update is called once per frame
    public void Initialize(Player player)
    {
        _player = player;
        moveLeftToAvoid = Random.Range(0f,1f) > 0.5f ? true : false;
    }

    void Update()
    {
        Vector3 toPlayer = (_player.transform.position - transform.position).normalized;
        Debug.DrawLine(transform.position, transform.position + toPlayer*avoidanceRadius);
    }

    void FixedUpdate()
    {
        if (sleepTimer > SLEEP_TIME)
        {
            sleep = false;
            sleepTimer = 0;
            return;
        }

        if (sleep)
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
                // Vector2 knockbackDirection = (Vector2)character.transform.position - (Vector2)transform.position;
                // float distance = knockbackDirection.magnitude;
                // knockbackDirection.Normalize();
                // character.MovementController.ApplyKnockback(knockbackDirection, Mathf.Log10(1f+ distance * knockbackAmount * 9f));

                Vector2 toCollision = (Vector2)character.transform.position - (Vector2)transform.position;
                if (Vector3.Dot(toPlayer.normalized, toCollision.normalized) > 0.7071f) // Check if would block path to player
                {
                    // return; // TODO: Alter direction or stop moving forward here.

                    moveDirection = moveLeftToAvoid ? Vector3.Cross(new Vector3(0, 0, -1), toPlayer) : Vector3.Cross(new Vector3(0, 0, 1), toPlayer);
                    Debug.LogError($"Avoiding enemy. Moving direction {moveDirection} instead of {MovementDirection}");
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
            blockedTimer += Time.fixedDeltaTime;
            if (blockedTimer >= 0.1f) return;
        }
        else
        {
            blockedTimer = 0;
        }
        
        Move(moveDirection * MovementSpeed * Time.fixedDeltaTime);
    }
}
