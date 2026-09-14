using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Sound Effect")]
public class SoundEffect : ScriptableObject
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private SoundType soundType = SoundType.SFX;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;
    [SerializeField] private float pitchMin = 0.9f;
    [SerializeField] private float pitchMax = 1.1f;
    [SerializeField] private bool loop = false;

    public AudioClip Clip =>
        clips != null && clips.Length > 0
            ? clips[Random.Range(0, clips.Length)]
            : null;
    public float Volume => volume;
    public float Pitch => Random.Range(pitchMin, pitchMax);
    public bool Loop => loop;
    public SoundType ClipSoundType => soundType;

    public enum SoundType
    {
        Music=0,
        SFX=1,
        UI=2
    }
}
