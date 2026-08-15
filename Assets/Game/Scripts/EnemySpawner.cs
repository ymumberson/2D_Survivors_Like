using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs = new();
    [SerializeField] private int spawnCount = 1;
    [SerializeField] private float spawnInterval = 5f;

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
        Instantiate(enemyPrefabs[0], this.transform);
    }
}
