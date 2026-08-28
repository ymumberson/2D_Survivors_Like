using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Player player;
    private Dictionary<LevelUpItem, int> levelUpItems = new();

    public void AddLevelUpItem(LevelUpItem item)
    {
        if (levelUpItems.ContainsKey(item))
        {
            levelUpItems[item]++;
        }
        else
        {
            levelUpItems[item] = 1;
        }

        item.Apply(player);
    }

    public void RemoveLevelUpItem(LevelUpItem item)
    {
        if (levelUpItems.ContainsKey(item))
        {
            levelUpItems[item] -= 1;

            if (levelUpItems[item] <= 0)
            {
                levelUpItems.Remove(item);
            }

            item.Remove(player);
        }
    }
}
