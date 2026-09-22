using System;
using TMPro;
using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI authorTMP;
    [SerializeField] private GameMusicController gameMusicController;

    void OnEnable()
    {
        gameMusicController.SongChanged += HandleSongChanged;
    }

    void OnDisable()
    {
        gameMusicController.SongChanged -= HandleSongChanged;
    }

    void Start()
    {
        UpdateUI(gameMusicController.CurrentSong);
    }

    private void HandleSongChanged(Music music)
    {
        UpdateUI(music);
    }

    private void UpdateUI(Music music)
    {
        if (music == null) return;

        if (titleTMP)
            titleTMP.text = music.Title;
        
        if (authorTMP)
            authorTMP.text = music.Author;
    }
}
