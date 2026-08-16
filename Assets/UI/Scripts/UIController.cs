using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private InGameOverlay inGameUI;
    [SerializeField] private GameOverMenu gameOverMenu;
    
    private InputAction openMenu;
    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    void Awake()
    {
        openMenu = InputSystem.actions.FindAction("OpenMenu");
        pauseMenu.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        gameController.GameEnded += EnableGameOverMenu;
    }

    void OnDisable()
    {
        gameController.GameEnded -= EnableGameOverMenu;
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

        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        _isPaused = true;
    }

    public void UnPauseGame()
    {
        if (!_isPaused) return;

        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        _isPaused = false;
    }

    private void EnableGameOverMenu()
    {
        UnPauseGame();

        gameOverMenu.gameObject.SetActive(true);
    }
}
