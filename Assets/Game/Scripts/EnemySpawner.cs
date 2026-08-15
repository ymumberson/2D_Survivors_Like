using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs = new();
    [SerializeField] private int spawnCount = 1;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private Dictionary<HealthController, GameObject> enemies = new();

    public Dictionary<HealthController, GameObject> Enemies => enemies;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        var spawnTimer = new WaitForSeconds(spawnInterval);
        while (true)
        {
            SpawnEnemies();
            yield return spawnTimer;
        }
    }

    private void SpawnEnemies()
    {
        for (int i=0; i<spawnCount; ++i)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(enemyPrefabs[0], this.transform);
        var healthController = enemy.GetComponentInChildren<HealthController>();
        enemies[healthController] = enemy;
        healthController.Died += () => enemies.Remove(healthController);
    }
}
