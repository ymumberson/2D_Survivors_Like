using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private PauseController pauseController;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private InGameOverlay inGameUI;
    [SerializeField] private GameOverMenu gameOverMenu;
    [SerializeField] private LevelUpMenu levelUpMenu;

    public void Initialize(Player player)
    {
        inGameUI.Initialise(player);
        levelUpMenu.Initialize(player, pauseController);
        gameOverMenu.Initialize(player, pauseController);
        pauseMenu.Initialize(pauseController);
    }
}
