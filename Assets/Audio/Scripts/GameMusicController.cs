using System.Collections;
using UnityEngine;

public class GameMusicController : MonoBehaviour
{
    [SerializeField] private SoundEffect[] gameMusic;

    void Start()
    {
        if (gameMusic == null || gameMusic.Length == 0 || !AudioManager.Instance) return;

        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        while (true)
        {
            var musicTrack = gameMusic[Random.Range(0, gameMusic.Length)];
            AudioManager.Instance.PlayMusic(musicTrack);
            yield return new WaitForSeconds(musicTrack.Clip.length);
        }
    }
}
