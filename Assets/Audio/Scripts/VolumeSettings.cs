using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider uiVolumeSlider;

    private float _masterVolume = 1f;
    private float _musicVolume = 1f;
    private float _sfxVolume = 1f;
    private float _uiVolume = 1f;

    void Awake()
    {
        _masterVolume = InitialiseSlider(AudioManager.MASTER_VOLUME_PLAYER_PREF, masterVolumeSlider, AudioManager.DEFAULT_MASTER_VOLUME);
        _musicVolume = InitialiseSlider(AudioManager.MUSIC_VOLUME_PLAYER_PREF, musicVolumeSlider, AudioManager.DEFAULT_MUSIC_VOLUME);
        _sfxVolume = InitialiseSlider(AudioManager.SFX_VOLUME_PLAYER_PREF, sfxVolumeSlider, AudioManager.DEFAULT_SFX_VOLUME);
        _uiVolume = InitialiseSlider(AudioManager.UI_VOLUME_PLAYER_PREF, uiVolumeSlider, AudioManager.DEFAULT_UI_VOLUME);
    }

    private float InitialiseSlider(string playerPrefsKey, Slider volumeSlider, float defaultVolume)
    {
        float loadedValue = PlayerPrefs.GetFloat(playerPrefsKey, defaultVolume);
        volumeSlider.SetValueWithoutNotify(loadedValue);
        return loadedValue;
    }

    public void OnMasterVolumeChanged(float value)
    {
        value = Mathf.Clamp01(value);
        _masterVolume = value;
        PlayerPrefs.SetFloat(AudioManager.MASTER_VOLUME_PLAYER_PREF, value);

        if (!AudioManager.Instance)
        {
            Debug.LogWarning("AudioManager is null!");
            return;
        }

        AudioManager.Instance.SetMasterVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        value = Mathf.Clamp01(value);
        _masterVolume = value;
        PlayerPrefs.SetFloat(AudioManager.MUSIC_VOLUME_PLAYER_PREF, value);

        if (!AudioManager.Instance)
        {
            Debug.LogWarning("AudioManager is null!");
            return;
        }

        AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        value = Mathf.Clamp01(value);
        _sfxVolume = value;
        PlayerPrefs.SetFloat(AudioManager.SFX_VOLUME_PLAYER_PREF, value);

        if (!AudioManager.Instance)
        {
            Debug.LogWarning("AudioManager is null!");
            return;
        }

        AudioManager.Instance.SetSFXVolume(value);
    }

    public void OnUIVolumeChanged(float value)
    {
        value = Mathf.Clamp01(value);
        _uiVolume = value;
        PlayerPrefs.SetFloat(AudioManager.UI_VOLUME_PLAYER_PREF, value);

        if (!AudioManager.Instance)
        {
            Debug.LogWarning("AudioManager is null!");
            return;
        }

        AudioManager.Instance.SetUIVolume(value);
    }
}
