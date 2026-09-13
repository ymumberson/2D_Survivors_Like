using Unity.VisualScripting;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private SoundEffect sound;

    public void PlaySFXSound()
    {
        if (!sound || !AudioManager.Instance)
        {
            LogWarning();
            return;
        }

        AudioManager.Instance.PlaySFX(sound);
    }

    public void PlayUISound()
    {
        if (!sound || !AudioManager.Instance)
        {
            LogWarning();
            return;
        }

        AudioManager.Instance.PlayUI(sound);
    }

    public void PlayMusicSound()
    {
        if (!sound || !AudioManager.Instance)
        {
            LogWarning();
            return;
        }

        AudioManager.Instance.PlayMusic(sound);
    }

    private void LogWarning()
    {
        Debug.LogWarning($"Sound effect not assigned on gameobject {gameObject.name}");
    }
}
