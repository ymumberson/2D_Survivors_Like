using UnityEngine;

public class MoveToPlayer : MovementController
{
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

        Vector2 toPlayer = (player.transform.position - transform.position).normalized * MovementSpeed * Time.deltaTime;

        Move(toPlayer);
    }
}
