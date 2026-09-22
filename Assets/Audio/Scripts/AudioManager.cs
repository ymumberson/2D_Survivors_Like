using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;
    [SerializeField] private AudioMixerGroup uiGroup;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource uiAudioSource;
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    public const string UI_VOLUME_KEY = "UIVolume";
    public const string MASTER_VOLUME_PLAYER_PREF = "MasterVolume";
    public const string MUSIC_VOLUME_PLAYER_PREF = "MusicVolume";
    public const string SFX_VOLUME_PLAYER_PREF = "SFXVolume";
    public const string UI_VOLUME_PLAYER_PREF = "UIVolume";
    public const float DEFAULT_MASTER_VOLUME = 0.5f;
    public const float DEFAULT_MUSIC_VOLUME = 0.5f;
    public const float DEFAULT_SFX_VOLUME = 0.5f;
    public const float DEFAULT_UI_VOLUME = 0.5f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitialiseFromPlayerPrefs();
    }

    private void InitialiseFromPlayerPrefs()
    {
        SetMasterVolume(PlayerPrefs.GetFloat(MASTER_VOLUME_PLAYER_PREF, DEFAULT_MASTER_VOLUME));
        SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME_PLAYER_PREF, DEFAULT_MUSIC_VOLUME));
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOLUME_PLAYER_PREF, DEFAULT_SFX_VOLUME));
        SetUIVolume(PlayerPrefs.GetFloat(UI_VOLUME_PLAYER_PREF, DEFAULT_UI_VOLUME));
    }

    public void Play(SoundEffect sound)
    {
        switch (sound.ClipSoundType)
        {
            case SoundEffect.SoundType.Music:
                PlayMusic(sound);
                break;
            default:
            case SoundEffect.SoundType.SFX:
                PlaySFX(sound);
                break;
            case SoundEffect.SoundType.UI:
                PlayUI(sound);
                break;
        }
    }

    private void PlayMusic(SoundEffect sound)
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

        musicAudioSource.Stop();
        ConfigureAudioSource(musicAudioSource, sound, musicGroup);
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
    
    private void PlaySFX(SoundEffect sound)
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

        ConfigureAudioSource(sfxAudioSource, sound, sfxGroup);
        sfxAudioSource.PlayOneShot(clip, sound.Volume);
    }

    private void PlayUI(SoundEffect sound)
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

        ConfigureAudioSource(uiAudioSource, sound, uiGroup);
        uiAudioSource.PlayOneShot(clip, sound.Volume);
    }

    private void ConfigureAudioSource(AudioSource source, SoundEffect sound, AudioMixerGroup group)
    {
        source.volume = sound.Volume;
        source.pitch = sound.Pitch;
        source.loop = sound.Loop;
        source.outputAudioMixerGroup = group;
    }

    public void SetMasterVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        audioMixer.SetFloat(MASTER_VOLUME_KEY, LinearToDecibels(volume));
    }
    
    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        audioMixer.SetFloat(MUSIC_VOLUME_KEY, LinearToDecibels(volume));
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        audioMixer.SetFloat(SFX_VOLUME_KEY, LinearToDecibels(volume));
    }

    public void SetUIVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        audioMixer.SetFloat(UI_VOLUME_KEY, LinearToDecibels(volume));
    }

    private float LinearToDecibels(float volume)
    {
        return volume <= 0.0001f
            ? -80f
            : Mathf.Log10(volume) * 20f;
    }
}
