using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner enemySpawner;

    private float _elapsedTime = 0f;
    public float ElapsedTime => _elapsedTime;

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

    void Update()
    {
        _elapsedTime += Time.deltaTime;
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
}
