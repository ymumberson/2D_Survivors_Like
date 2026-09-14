using System.Collections;
using UnityEngine;

public class GameMusicController : MonoBehaviour
{
    [SerializeField] private SoundEffect[] gameMusic;

    private void Start()
    {
        if (gameMusic == null || gameMusic.Length == 0 || !AudioManager.Instance) return;

        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        SoundEffect previousTrack = null;
        
        while (true)
        {
            ShuffleMusic();

            if (gameMusic.Length > 1 && gameMusic[0] == previousTrack)
            {
                int swapIndex = Random.Range(1, gameMusic.Length);
                (gameMusic[0], gameMusic[swapIndex]) = (gameMusic[swapIndex], gameMusic[0]);
            }

            for (int i=0; i<gameMusic.Length; ++i)
            {
                SoundEffect musicTrack = gameMusic[i];
                previousTrack = musicTrack;
                AudioManager.Instance.Play(musicTrack);
                yield return new WaitForSeconds(musicTrack.Clip.length);
            }
        }
    }

    private void ShuffleMusic()
    {
        if (gameMusic == null || gameMusic.Length <= 1) return;

        for (int i = gameMusic.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (gameMusic[i], gameMusic[j]) = (gameMusic[j], gameMusic[i]);
        }
    }
}
