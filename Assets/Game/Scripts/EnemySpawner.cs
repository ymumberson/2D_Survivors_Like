using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs = new();
    [SerializeField] private GameObject enemySpawnWarningIndicatorPrefab;
    [SerializeField] private float warningDuration = 1f;
    [SerializeField] private int spawnCount = 1;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] Bounds spawnBounds = new();
    private Dictionary<HealthController, GameObject> enemies = new();

    public Dictionary<HealthController, GameObject> Enemies => enemies;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemies()
    {
        for (int i=0; i<spawnCount; ++i)
        {
            StartCoroutine(SpawnEnemyAfterWarning());
        }
    }

    private IEnumerator SpawnEnemyAfterWarning()
    {
        Vector3 spawnLocation = GenerateSpawnPosition();
        
        if (warningDuration > 0 && enemySpawnWarningIndicatorPrefab)
        {
            GameObject warningIndicator = Instantiate(enemySpawnWarningIndicatorPrefab, this.transform);
            warningIndicator.transform.position = spawnLocation;
            yield return new WaitForSeconds(warningDuration);
            Destroy(warningIndicator);
            SpawnEnemy(spawnLocation);
        }
        else
        {
            SpawnEnemy(spawnLocation);
        }
    }

    private void SpawnEnemy(Vector3 spawnLocation)
    {
        var enemy = Instantiate(enemyPrefabs[0], this.transform);
        enemy.transform.position = spawnLocation;
        var healthController = enemy.GetComponentInChildren<HealthController>();
        enemies[healthController] = enemy;
        healthController.Died += () => enemies.Remove(healthController);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float randomX = transform.position.x + UnityEngine.Random.Range(0, spawnBounds.extents.x * 2) + spawnBounds.min.x;
        float randomY = transform.position.y + UnityEngine.Random.Range(0, spawnBounds.extents.y * 2) + spawnBounds.min.y;
        return new Vector3(randomX, randomY, 0);
    }
}
