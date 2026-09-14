using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image rarityBackground;
    [SerializeField] private TextMeshProUGUI rarity;
    [SerializeField] private Sprite fallbackImage;
    [SerializeField] private Button button;
    [SerializeField] private Color commonColor = Color.white;
    [SerializeField] private Color uncommonColor = Color.white;
    [SerializeField] private Color rareColor = Color.white;
    [SerializeField] private Color legendaryColor = Color.white;
    private UnityAction _previousOnClick;

    public void SetContent(LevelUpItem levelUpItem, UnityAction onClick)
    {
        title.text = levelUpItem.title ?? "Missing Title";
        image.sprite = levelUpItem.image != null ? levelUpItem.image : fallbackImage;
        description.text = levelUpItem.description ?? "Missing Description";
        rarity.text = levelUpItem.rarity.ToString() ?? "Missing Rarity";

        if (_previousOnClick != null)
            button.onClick.RemoveListener(_previousOnClick);
        
        button.onClick.AddListener(onClick);
        _previousOnClick = onClick;

        switch (levelUpItem.rarity)
        {
            default:
            case LevelUpItem.Rarity.Common:
                rarityBackground.color = commonColor;
                break;
            case LevelUpItem.Rarity.Uncommon:
                rarityBackground.color = uncommonColor;
                break;
            case LevelUpItem.Rarity.Rare:
                rarityBackground.color = rareColor;
                break;
            case LevelUpItem.Rarity.Legendary:
                rarityBackground.color = legendaryColor;
                break;
        }
    }
}
