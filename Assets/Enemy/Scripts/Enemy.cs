using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyDeathHandler enemyDeathHandler;
    [SerializeField] private ContactDamage contactDamage;
    private Player _player;
    private bool isInitialized;
    public EnemyDeathHandler EnemyDeathHandler => enemyDeathHandler;

    public void Initialize(Player player)
    {
        _player = player;
        isInitialized = true;

        (MovementController as MoveToPlayer).Initialize(_player);
        contactDamage.Initialize(AttackController);

        gameObject.SetActive(true);
    }
}
