using UnityEngine;

public abstract class LevelUpItem : ScriptableObject
{
    public string title;
    public string description;
    public Sprite image;
    public Rarity rarity;

    public abstract void Apply(Player player);
    public abstract void Remove(Player player);

    public enum Rarity
    {
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Legendary = 3
    }

    public bool HasAllContent => (
        !string.IsNullOrEmpty(title) && 
        !string.IsNullOrEmpty(description) && 
        image != null
    );
}
