using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Music")]
public class Music : SoundEffect
{
    [SerializeField] private string title;
    [SerializeField] private string author;
    [SerializeField] private string url;

    public string Title => title;
    public string Author => author;
    public string URL => url;
}
