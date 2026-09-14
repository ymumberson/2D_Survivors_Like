using Unity.VisualScripting;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private SoundEffect sound;
    [SerializeField] private bool playOnEnable = false;
    [SerializeField] private bool playOnDisable = false;

    void OnEnable()
    {
        if (playOnEnable)
            Play();
    }

    void OnDisable()
    {
        if (playOnDisable)
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
