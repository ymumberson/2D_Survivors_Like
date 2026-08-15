using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private EnemySpawner enemySpawner;
    private HealthController _player;

    void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void FindPlayer()
    {
        var players = FindObjectsByType<HealthController>(FindObjectsSortMode.None).Where((HealthController hc) => hc.tag == "Player");
        _player = players.First();
    }

    public HealthController GetPlayer()
    {
        if (!_player)
            FindPlayer();

        return _player;
    }

    public Dictionary<HealthController, GameObject> GetEnemies()
    {
        if (!enemySpawner) return null;

        return enemySpawner.Enemies;
    }
}
