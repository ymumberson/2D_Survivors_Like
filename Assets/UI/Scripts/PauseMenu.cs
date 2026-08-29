using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private PauseController _pauseController;
    private bool isInitialized;
    private InputAction openMenu;
    private bool _isPaused = false;

    void Awake()
    {
        openMenu = InputSystem.actions.FindAction("OpenMenu");
        pausePanel.SetActive(false);
    }

    public void Initialize(PauseController pauseController)
    {
        _pauseController = pauseController;
        isInitialized = true;
    }

    void Update()
    {
        CheckOpenMenu();
    }

    public void CheckOpenMenu()
    {
        if (!openMenu.WasPressedThisFrame()) return;

        if (_isPaused)
        {
            _pauseController.ReleasePause();
            _isPaused = false;
        }
        else
        {
            _pauseController.RequestPause();
            _isPaused = true;
            
        } 
        pausePanel.SetActive(_isPaused);
    }
}
