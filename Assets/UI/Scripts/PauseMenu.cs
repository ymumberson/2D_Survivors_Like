using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private PauseController _pauseController;
    private GameFlowController _gameFlowController;
    private InputAction openMenu;
    private bool _isPaused = false;

    void Awake()
    {
        openMenu = InputSystem.actions.FindAction("OpenMenu");
        pausePanel.SetActive(false);
    }

    public void Initialize(PauseController pauseController, GameFlowController gameFlowController)
    {
        _pauseController = pauseController;
        _gameFlowController = gameFlowController;
    }

    void Update()
    {
        CheckOpenMenu();
    }

    private void CheckOpenMenu()
    {
        if (!openMenu.WasPressedThisFrame()) return;

        if (_isPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        } 
    }

    private void Pause()
    {
        if (_isPaused) return;

        _pauseController.RequestPause();
        _isPaused = true;
        pausePanel.SetActive(true);
    }

    private void Unpause()
    {
        if (!_isPaused) return;

        _pauseController.ReleasePause();
        _isPaused = false;
        pausePanel.SetActive(false);
    }

    public void OnContinue()
    {
        Unpause();
    }

    public void OnOptions()
    {
        
    }

    public void OnRestart()
    {
        _gameFlowController.RestartGame();
    }
}
