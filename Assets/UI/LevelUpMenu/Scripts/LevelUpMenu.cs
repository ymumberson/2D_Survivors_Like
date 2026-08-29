using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelUpItemPrefab;
    [SerializeField] private GameObject levelUpMenuPanel;
    private PauseController _pauseController;
    private Player _player;
    private bool isInitialized = false;
    private bool isSubscribed = false;

    public void Initialize(Player player, PauseController pauseController)
    {
        _player = player;
        _pauseController = pauseController;
        isInitialized = true;
        TrySubscribe();
        levelUpMenuPanel.SetActive(false);
    }

    void OnEnable()
    {
        TrySubscribe();
    }

    void OnDisable()
    {
        TryUnsubscribe();
    }

    private bool TrySubscribe()
    {
        if (isSubscribed || !isInitialized || !isActiveAndEnabled) return false;

        _player.ExperienceController.LevelledUp += HandlePlayerLevelledUp;

        isSubscribed = true;

        return true;
    }

    private bool TryUnsubscribe()
    {
        if (!isSubscribed) return false;

        _player.ExperienceController.LevelledUp -= HandlePlayerLevelledUp;

        isSubscribed = false;

        return true;
    }

    private void HandlePlayerLevelledUp(int newLevel)
    {
        levelUpMenuPanel.SetActive(true);
        _pauseController.RequestPause();
    }
}
