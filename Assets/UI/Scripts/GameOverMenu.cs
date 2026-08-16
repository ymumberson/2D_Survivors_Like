using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private float previousTimeScale = 1;

    void OnEnable()
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = previousTimeScale;
    }
}
