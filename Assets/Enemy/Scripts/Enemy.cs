using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyDeathHandler enemyDeathHandler;
    [SerializeField] private ContactDamage contactDamage;
    private Player _player;
    public EnemyDeathHandler EnemyDeathHandler => enemyDeathHandler;

    public void Initialize(Player player)
    {
        _player = player;

        (MovementController as MoveToPlayer).Initialize(_player);
        contactDamage.Initialize(AttackController, null);

        gameObject.SetActive(true);
    }
}
