using UnityEngine;

public class MoveToPlayer : MovementController
{
    private Player _player;
    
    // Update is called once per frame
    public void Initialize(Player player)
    {
        _player = player;
    }
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!_player) return;

        if (!_player.HealthController || _player.HealthController.IsDead) return;

        Vector2 toPlayer = (_player.transform.position - transform.position).normalized * MovementSpeed * Time.deltaTime;

        Move(toPlayer);
    }
}
