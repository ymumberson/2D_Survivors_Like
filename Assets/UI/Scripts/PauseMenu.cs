using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    private PauseController _pauseController;
    private GameFlowController _gameFlowController;
    private InputAction openMenu;
    private bool _isPaused = false;
    private Stack<GameObject> openPanels = new();

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
            CloseMostRecentPanel();
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
        OpenPanel(pausePanel);
    }

    private void Unpause()
    {
        if (!_isPaused) return;

        _pauseController.ReleasePause();
        _isPaused = false;
        
        while (openPanels.Count > 0)
        {
            openPanels.Pop().SetActive(false);
        }
    }

    public void CloseMostRecentPanel()
    {
        if (openPanels.Count == 0)
        {
            Unpause();
            return;
        }

        GameObject mostRecentPanel = openPanels.Pop();
        mostRecentPanel.SetActive(false);

        if (openPanels.Count <= 0)
        {
            Unpause();
        }
        else
        {
            GameObject nextPanel = openPanels.Peek();
            nextPanel.SetActive(true);
        }
    }

    private void OpenPanel(GameObject panel)
    {
        if (openPanels.Count > 0)
        {
            GameObject currentPanel = openPanels.Peek();
            currentPanel.SetActive(false);
        }

        panel.SetActive(true);
        openPanels.Push(panel);
    }

    public void OnContinue()
    {
        Unpause();
    }

    public void OnOptions()
    {
        OpenPanel(settingsPanel);
    }

    public void OnRestart()
    {
        _gameFlowController.RestartGame();
    }
}
