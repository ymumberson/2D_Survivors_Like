using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> disableOnDied = new();
    private HealthController _healthController;

    void Awake()
    {
        _healthController = GetComponentInChildren<HealthController>();
    }

    void OnEnable()
    {
        _healthController.Died += OnDied;
        _healthController.Revived += OnRevived;
    }

    void OnDisable()
    {
        _healthController.Died -= OnDied;
        _healthController.Revived -= OnRevived;
    }

    private void OnDied()
    {
        foreach (GameObject go in disableOnDied)
        {
            go.SetActive(false);
        }
    }

    private void OnRevived()
    {
        foreach (GameObject go in disableOnDied)
        {
            go.SetActive(true);
        }
    }
}
