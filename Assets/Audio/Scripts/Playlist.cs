using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Playlist : MonoBehaviour
{
    [SerializeField] private Music[] musicList;
    private Music currentSong;
    private Stack<Music> previousMusic = new();
    private Stack<Music> nextMusic = new();
    private int nextSong = 0;

    public string SongTitle => currentSong != null ? currentSong.Title : "";
    public string SongAuthor => currentSong != null ? currentSong.Author : "";
    public string SongURL => currentSong != null ? currentSong.URL : "";
    public int debugIndex => nextSong;
    public string NextSongTitle => nextMusic.Count > 0 ? nextMusic.Peek().Title : musicList[nextSong].Title;
    public string PreviousSongTitle => previousMusic.Count > 0 ? previousMusic.Peek().Title : "";

    void Awake()
    {
        ShuffleMusic();
        Next();
    }

    public Music Next()
    {
        // Push current song onto stack of previous songs
        if (currentSong != null)
            previousMusic.Push(currentSong);

        if (nextMusic.Count > 0) // If we used Previous, then return to the song we were on
        {
            currentSong = nextMusic.Pop();
        }
        else // Otherwise, select a new song
        {
            if (nextSong >= musicList.Length - 1)
                ShuffleMusic();
            currentSong = musicList[nextSong];
            nextSong = WrapIndex(nextSong+1);
        }

        return currentSong;
    }

    public Music Previous()
    {
        if (previousMusic.Count == 0) return null;

        // Store the song we're currently on so we play it next
        if (currentSong != null)
            nextMusic.Push(currentSong);

        // If we've already played a song, then go through the list in order
        if (previousMusic.Count > 0)
        {
            currentSong = previousMusic.Pop();
        }

        // Will return the current song if no others have been played
        return currentSong;
    }

    private int WrapIndex(int index)
    {
        if (index < 0)
            return musicList.Length-1;
        
        if (index >= musicList.Length)
            return 0;
        
        return index;
    }

    public void ShuffleMusic()
    {
        for (int i=musicList.Length-1; i>0; --i)
        {
            int j = Random.Range(0, i+1);
            (musicList[i], musicList[j]) = (musicList[j], musicList[i]);
        }

        // Avoid first shuffled song being same as last song played
        if (musicList.Length > 1 && musicList[0] == currentSong)
        {
            int j = Random.Range(1, musicList.Length);
            (musicList[j], musicList[0]) = (musicList[0], musicList[j]);
        }

        nextSong = 0;
    }
}

[CustomEditor(typeof(Playlist))]
public class PlaylistEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        Playlist playlist = (Playlist)target;

        GUILayout.Label($"Current: {playlist.SongTitle}");

        if (GUILayout.Button("Next"))
        {
            playlist.Next();
        }

        if (GUILayout.Button("Previous"))
        {
            playlist.Previous();
        }
    }
}
