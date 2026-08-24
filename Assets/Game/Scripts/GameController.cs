using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float setTimeScale = 1;
    
    public static GameController Instance;
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner enemySpawner;
    private const float DIFFICULTY_SCALING_FACTOR = 60f; // Larger = slower scaling
    private int difficultyLevel;
    private float _elapsedTime = 0f;
    public float ElapsedTime => _elapsedTime;
    public int DifficultyLevel => difficultyLevel;

    public event Action GameEnded;
    public event Action<int> DifficultyChanged;

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

        CapFPS();
    }

    void OnEnable()
    {
        GetPlayer().GetComponentInChildren<HealthController>().Died += HandlePlayerDied;
    }

    void OnDisable()
    {
        GetPlayer().GetComponentInChildren<HealthController>().Died -= HandlePlayerDied;
    }

    void Update()
    {
        Time.timeScale = setTimeScale;
        
        _elapsedTime += Time.deltaTime;
        int newDifficulty = GetDifficultyLevel();
        if (newDifficulty != difficultyLevel)
        {
            difficultyLevel = newDifficulty;
            DifficultyChanged?.Invoke(difficultyLevel);
        }
    }

    public Player GetPlayer()
    {
        return _player;
    }

    public Dictionary<HealthController, GameObject> GetEnemies()
    {
        if (!enemySpawner) return null;

        return enemySpawner.Enemies;
    }

    public Vector3 GetClosestTarget(Vector3 startPosition)
    {
        float closestDistance = float.PositiveInfinity;
        Vector3 closestTarget = Vector3.zero;

        var enemies = GetEnemies();
        if (enemies == null) return closestTarget;

        foreach (GameObject enemyGO in enemies.Values)
        {
            float distance = Vector3.Distance(startPosition, enemyGO.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = enemyGO.transform.position;
            }
        }

        return closestTarget;
    }

    public Vector3 GetRandomTarget()
    {
        var enemies = GetEnemies();
        if (enemies == null) return Vector3.zero;

        int randomIndex = UnityEngine.Random.Range(0, enemies.Count -1);
        return enemies[enemies.Keys.ElementAt(randomIndex)].transform.position;
    }

    private void CapFPS()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }

    private void HandlePlayerDied()
    {
        GameEnded?.Invoke();
    }

    private int GetDifficultyLevel()
    {
        return Mathf.FloorToInt(ElapsedTime / DIFFICULTY_SCALING_FACTOR);
    }
}
