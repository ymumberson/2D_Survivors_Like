using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider uiVolumeSlider;

    private const string MASTER_VOLUME_PLAYER_PREF = "MasterVolume";
    private const string MUSIC_VOLUME_PLAYER_PREF = "MusicVolume";
    private const string SFX_VOLUME_PLAYER_PREF = "SFXVolume";
    private const string UI_VOLUME_PLAYER_PREF = "UIVolume";
    private const float DEFAULT_VOLUME = 1f;

    private float _masterVolume = 1f;
    private float _musicVolume = 1f;
    private float _sfxVolume = 1f;
    private float _uiVolume = 1f;

    void Awake()
    {
        _masterVolume = InitialiseSlider(MASTER_VOLUME_PLAYER_PREF, masterVolumeSlider);
        _musicVolume = InitialiseSlider(MUSIC_VOLUME_PLAYER_PREF, musicVolumeSlider);
        _sfxVolume = InitialiseSlider(SFX_VOLUME_PLAYER_PREF, sfxVolumeSlider);
        _uiVolume = InitialiseSlider(UI_VOLUME_PLAYER_PREF, uiVolumeSlider);
    }

    private float InitialiseSlider(string playerPrefsKey, Slider volumeSlider)
    {
        float loadedValue = PlayerPrefs.GetFloat(playerPrefsKey, DEFAULT_VOLUME);
        volumeSlider.SetValueWithoutNotify(loadedValue);
        return loadedValue;
    }

    public void OnMasterVolumeChanged(float value)
    {
        value = Mathf.Clamp01(value);
        _masterVolume = value;
        PlayerPrefs.SetFloat(MASTER_VOLUME_PLAYER_PREF, value);

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
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PLAYER_PREF, value);

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
        PlayerPrefs.SetFloat(SFX_VOLUME_PLAYER_PREF, value);

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
        PlayerPrefs.SetFloat(UI_VOLUME_PLAYER_PREF, value);

        if (!AudioManager.Instance)
        {
            Debug.LogWarning("AudioManager is null!");
            return;
        }

        AudioManager.Instance.SetUIVolume(value);
    }
}
