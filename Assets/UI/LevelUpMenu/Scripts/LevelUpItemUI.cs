using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Sprite fallbackImage;
    private LevelUpItem _levelUpItem;

    public void SetContent(LevelUpItem levelUpItem)
    {
        _levelUpItem = levelUpItem;

        title.text = levelUpItem.title ?? "Missing Title";
        image.sprite = levelUpItem.image != null ? levelUpItem.image : fallbackImage;
        description.text = levelUpItem.description ?? "Missing Description";
    }
}
