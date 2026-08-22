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
    [SerializeField] BoxCollider2D spawnBounds = new();
    [SerializeField] private float minSpawnDistanceFromPlayer = 2f;
    [SerializeField] private float maxSpawnDistanceFromPlayer = 2f;
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
        const int MAX_TRIES = 10;
        for (int i=0; i<MAX_TRIES; ++i)
        {
            Vector3 randomPosition = GenerateOffscreenPosition();
            if (spawnBounds.bounds.Contains(randomPosition))
            {
                return randomPosition;
            }
        }

        return Vector3.zero;
    }

    private Vector3 GenerateOffscreenPosition()
    {
        Camera mainCamera = Camera.main;
        if (!mainCamera) return Vector3.zero;

        int side = UnityEngine.Random.Range(0,4);
        Vector3 position = new Vector3();

        float spawnDistanceFromPlayer = UnityEngine.Random.Range(minSpawnDistanceFromPlayer, maxSpawnDistanceFromPlayer);

        switch (side)
        {
            case 0: // Top
                position = mainCamera.ViewportToWorldPoint(
                    new Vector3(UnityEngine.Random.value, 1f, 0)
                );
                position.y += spawnDistanceFromPlayer;
                break;
            case 1: // Bottom
                position = mainCamera.ViewportToWorldPoint(
                    new Vector3(UnityEngine.Random.value, 0, 0)
                );
                position.y -= spawnDistanceFromPlayer;
                break;
            case 2: // Left
                position = mainCamera.ViewportToWorldPoint(
                    new Vector3(0, UnityEngine.Random.value, 0)
                );
                position.x -= spawnDistanceFromPlayer;
                break;
            case 3: // Right
                position = mainCamera.ViewportToWorldPoint(
                    new Vector3(1f, UnityEngine.Random.value, 0)
                );
                position.x += spawnDistanceFromPlayer;
                break;
        }
        position.z = 0;

        return position;
    }
}
