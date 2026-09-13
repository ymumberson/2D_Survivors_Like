using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource uiAudioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //TODO: Add audio sources into the mixer
    }

    public void PlayMusic(SoundEffect sound)
    {
        if (!musicAudioSource)
        {
            Debug.LogWarning("Music audio source not assigned!");
            return;
        }

        AudioClip clip = sound.Clip;
        if (clip == null)
        {
            Debug.LogWarning($"Sound effect ${sound.name} is missing a clip.");
            return;
        }

        ConfigureAudioSource(musicAudioSource, sound);
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
    
    public void PlaySFX(SoundEffect sound)
    {
        if (!sfxAudioSource)
        {
            Debug.LogWarning("SFX audio source not assigned!");
            return;
        }

        AudioClip clip = sound.Clip;
        if (clip == null)
        {
            Debug.LogWarning($"Sound effect ${sound.name} is missing a clip.");
            return;
        }

        ConfigureAudioSource(sfxAudioSource, sound);
        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlayUI(SoundEffect sound)
    {
        if (!uiAudioSource)
        {
            Debug.LogWarning("UI audio source not assigned!");
            return;
        }

        AudioClip clip = sound.Clip;
        if (clip == null)
        {
            Debug.LogWarning($"Sound effect ${sound.name} is missing a clip.");
            return;
        }

        ConfigureAudioSource(uiAudioSource, sound);
        uiAudioSource.PlayOneShot(clip);
    }

    private void ConfigureAudioSource(AudioSource source, SoundEffect sound)
    {
        source.volume = sound.Volume;
        source.pitch = sound.Pitch;
        source.loop = sound.Loop;
    }

    public void SetMasterVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        //TODO set volume
        Debug.Log($"Setting master volume to {volume}");
    }
    
    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        //TODO set volume
        Debug.Log($"Setting music volume to {volume}");
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        //TODO set volume
        Debug.Log($"Setting sfx volume to {volume}");
    }

    public void SetUIVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        //TODO set volume
        Debug.Log($"Setting ui volume to {volume}");
    }
}
