using UnityEngine;

public abstract class LevelUpItem : ScriptableObject
{
    public string title;
    public string description;
    public Sprite image;

    public abstract void Apply(Player player);
    public abstract void Remove(Player player);

    public bool HasAllContent => (
        !string.IsNullOrEmpty(title) && 
        !string.IsNullOrEmpty(description) && 
        image != null
    );
}
