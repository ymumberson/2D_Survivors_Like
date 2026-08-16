using UnityEngine;

public class MoveToPlayer : MovementController
{
    [SerializeField] private float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Player player = GameController.Instance.GetPlayer();
        if (!player) return;

        HealthController healthController = player.GetComponentInChildren<HealthController>();
        if (!healthController || healthController.IsDead) return;

        Vector2 toPlayer = (player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;

        Move(toPlayer);
    }
}
