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
    private const float DIFFICULTY_SCALING_FACTOR = 30f;
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
