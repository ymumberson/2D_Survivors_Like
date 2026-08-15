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
        HealthController player = GameController.Instance.GetPlayer();

        if (!player || player.IsDead) return;

        Vector2 toPlayer = (player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;

        Move(toPlayer);
    }
}
