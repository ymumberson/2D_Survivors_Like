using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private Player _player;
    private PauseController _pauseController;
    private GameFlowController _gameFlowController;
    private bool _isInitialized;
    private bool _isSubscribed;
    
    public void Initialize(Player player, PauseController pauseController, GameFlowController gameFlowController)
    {
        _player = player;
        _pauseController = pauseController;
        _gameFlowController = gameFlowController;
        _isInitialized = true;
        TrySubscribe();
    }

    void OnEnable()
    {
        TrySubscribe();
    }

    void OnDisable()
    {
        TryUnsubscribe();
    }

    private void TrySubscribe()
    {
        if (_isSubscribed || !_isInitialized) return;

        _player.HealthController.Died += HandlePlayerDied;

        _isSubscribed = true;
    }

    private void TryUnsubscribe()
    {
        if (!_isSubscribed) return;

        _player.HealthController.Died -= HandlePlayerDied;

        _isSubscribed = false;
    }

    private void HandlePlayerDied()
    {
        gameObject.SetActive(true);
        _pauseController.RequestPause();
    }

    public void OnRestart()
    {
        _gameFlowController.RestartGame();
    }
}
