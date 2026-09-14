using Unity.VisualScripting;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private SoundEffect sound;
    [SerializeField] private bool playOnEnable = false;

    void OnEnable()
    {
        if (playOnEnable)
            Play();
    }

    public void Play()
    {
        if (!sound || !AudioManager.Instance)
        {
            Debug.LogWarning($"Sound effect not assigned on gameobject {gameObject.name}");
            return;
        }

        AudioManager.Instance.Play(sound);
    }
}
