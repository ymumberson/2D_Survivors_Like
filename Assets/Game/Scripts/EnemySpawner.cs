using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs = new();
    [SerializeField] private GameObject enemySpawnWarningIndicatorPrefab;
    [SerializeField] private float warningDuration = 1f;
    [SerializeField] private int baseSpawnCount = 1;
    [SerializeField] private float baseSpawnInterval = 5f;
    [SerializeField] private float minSpawnInterval = 0.5f;
    [SerializeField] BoxCollider2D spawnBounds = new();
    [SerializeField] private float minSpawnDistanceFromPlayer = 2f;
    [SerializeField] private float maxSpawnDistanceFromPlayer = 2f;
    [SerializeField] private float healthScaler = 10f;
    [SerializeField] private float attackScaler = 20f;
    private GameController _gameController;
    private Player _player;
    private bool _isInitialized;
    private bool _isSubscribed;
    private float spawnInterval;
    private int spawnCount;
    private Dictionary<HealthController, GameObject> enemies = new();

    public Dictionary<HealthController, GameObject> Enemies => enemies;

    void Awake()
    {
        spawnInterval = baseSpawnInterval;
        spawnCount = baseSpawnCount;
    }

    public void Initialize(GameController gameController, Player player)
    {
        _gameController = gameController;
        _player = player;
        _isInitialized = true;

        TrySubscribe();
        StartCoroutine(SpawnLoop());
    }

    void OnEnable()
    {
        TrySubscribe();
    }

    void OnDisable()
    {
        TryUnsubscribe();
    }

    private bool TrySubscribe()
    {
        if (_isSubscribed || !_isInitialized || !isActiveAndEnabled) return false;

        _gameController.DifficultyChanged += HandleDifficultyChanged;

        _isSubscribed = true;

        return true;
    }

    private bool TryUnsubscribe()
    {
        if (!_isSubscribed) return false;

        _gameController.DifficultyChanged -= HandleDifficultyChanged;

        _isSubscribed = false;

        return true;
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
        var enemyPrefab = GetEnemyForDifficulty(_gameController.DifficultyLevel);
        var enemyGO = Instantiate(enemyPrefab, this.transform);
        enemyGO.transform.position = spawnLocation;
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.Initialize(_player);
        var healthController = enemy.HealthController;
        enemies[healthController] = enemyGO;
        healthController.Died += () => enemies.Remove(healthController);

        ScaleEnemyDifficulty(enemy);
    }

    private GameObject GetEnemyForDifficulty(int difficultyLevel)
    {
        float difficultyPerTier = 20f;
        float targetTier = Mathf.Clamp(
            difficultyLevel / difficultyPerTier,
            0f,
            enemyPrefabs.Count - 1
        );

        float spread = 0.25f;

        List<float> weights = new();

        float totalWeight = 0f;

        for (int i=0; i<enemyPrefabs.Count; ++i)
        {
            float distance = i - targetTier;

            float weight = Mathf.Exp(
                -(distance * distance) / spread
            );

            weights.Add(weight);
            totalWeight += weight;
        }

        float randomValue = UnityEngine.Random.value * totalWeight;

        for (int i=0; i<weights.Count; ++i)
        {
            randomValue -= weights[i];

            if (randomValue <= 0f)
            {
                return enemyPrefabs[i];
            }
        }
        return enemyPrefabs[^1];
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

    private void ScaleEnemyDifficulty(Enemy enemy)
    {
        float difficulty = _gameController.DifficultyLevel;

        if (enemy.HealthController)
        {
            enemy.HealthController.IncreaseMaxHealth(difficulty / healthScaler);
        }

        if (enemy.AttackController)
        {
            enemy.AttackController.IncrementDamageMultiplier(difficulty / attackScaler);
        }

        if (enemy.EnemyDeathHandler)
        {
            enemy.EnemyDeathHandler.SetExperienceDropMultiplier(1 + Mathf.Sqrt(difficulty));
        }
    }

    private void HandleDifficultyChanged(int newDifficultyLevel)
    {
        spawnInterval = Mathf.Max(
            minSpawnInterval,
            CalculateSpawnInterval(newDifficultyLevel)
        );

        spawnCount = baseSpawnCount + Mathf.FloorToInt(newDifficultyLevel / 2f);
    }

    private float CalculateSpawnInterval(int difficultyLevel)
    {
        return baseSpawnInterval - difficultyLevel / 4;
    }
}
