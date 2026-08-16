using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject inGameUI;
    
    private InputAction openMenu;
    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    void Awake()
    {
        openMenu = InputSystem.actions.FindAction("OpenMenu");
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        CheckOpenMenu();
    }

    public void CheckOpenMenu()
    {
        if (!openMenu.WasPressedThisFrame()) return;

        if (_isPaused)
            UnPauseGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        if (_isPaused) return;

        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        _isPaused = true;
    }

    public void UnPauseGame()
    {
        if (!_isPaused) return;

        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _isPaused = false;
    }
}
