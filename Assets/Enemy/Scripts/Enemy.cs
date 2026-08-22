using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyDeathHandler enemyDeathHandler;
    public EnemyDeathHandler EnemyDeathHandler => enemyDeathHandler;
}
