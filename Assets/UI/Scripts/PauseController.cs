using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private const float PAUSE_TIME_SCALE = 0;
    private int _pauseRequests;
    private float _previousTimeScale = 1f;
    private bool _isPaused;

    public bool IsPaused => _isPaused;

    public void RequestPause()
    {
        _pauseRequests++;

        if (_pauseRequests == 1) // Pause
        {
            // Store previous timescale when first pausing so that we can restore it
            _previousTimeScale = Time.timeScale;
            Time.timeScale = PAUSE_TIME_SCALE;
            _isPaused = true;
        }
    }

    public void ReleasePause()
    {
        _pauseRequests = Mathf.Max(0, _pauseRequests - 1);

        if (_pauseRequests == 0) // Unpause
        {
            // Restore previous timescale
            Time.timeScale = _previousTimeScale;
            _isPaused = false;
        }
    }
}
