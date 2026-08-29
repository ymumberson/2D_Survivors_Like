using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI rarity;
    [SerializeField] private Sprite fallbackImage;
    [SerializeField] private Button button;
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
    }
}
