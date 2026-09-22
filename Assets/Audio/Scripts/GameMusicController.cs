using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class GameMusicController : MonoBehaviour
{
    [SerializeField] private Playlist gameMusic;
    private float timer = 0;
    private float songDuration = 0;

    public Music CurrentSong => gameMusic.CurrentSong;

    public event Action<Music> SongChanged;

    private void Start()
    {
        if (gameMusic == null || gameMusic.songCount == 0 || !AudioManager.Instance)
            enabled = false;
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer < songDuration) return;

        Next();
    }

    public void Next()
    {
        SetSong(gameMusic.Next());
    }

    public void Previous()
    {
        SetSong(gameMusic.Previous());
    }

    private void SetSong(Music song)
    {
        if (song == null) return;

        AudioManager.Instance.Play(song);
        songDuration = song.Clip.length;
        timer = 0;

        SongChanged.Invoke(song);
    }
}

[CustomEditor(typeof(GameMusicController))]
public class GameMusicControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        GameMusicController controller = (GameMusicController)target;

        GUILayout.Label($"Current: {controller.CurrentSong.Title}");

        if (GUILayout.Button("Next"))
        {
            controller.Next();
        }

        if (GUILayout.Button("Previous"))
        {
            controller.Previous();
        }
    }
}
