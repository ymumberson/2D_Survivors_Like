using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int uncommonThreshold;
    [SerializeField, Range(0, 100)] private int rareThreshold;
    [SerializeField, Range(0, 100)] private int legendaryThreshold;
    [SerializeField] private List<LevelUpItem> levelUpItemPool = new();
    private Dictionary<LevelUpItem, int> uncommonItemPool = new();
    private Dictionary<LevelUpItem, int> commonItemPool = new();
    private Dictionary<LevelUpItem, int> rareItemPool = new();
    private Dictionary<LevelUpItem, int> legendaryItemPool = new();

    public int Count => (
        uncommonItemPool.Count + 
        commonItemPool.Count +
        rareItemPool.Count +
        legendaryItemPool.Count
    );

    void Awake()
    {
        InitialiseItemPools();
    }

    private void InitialiseItemPools()
    {
        foreach (LevelUpItem item in levelUpItemPool)
        {
            switch (item.rarity)
            {
                default:
                case LevelUpItem.Rarity.Common:
                    AddItemToPool(item, commonItemPool);
                    break;
                case LevelUpItem.Rarity.Uncommon:
                    AddItemToPool(item, uncommonItemPool);
                    break;
                case LevelUpItem.Rarity.Rare:
                    AddItemToPool(item, rareItemPool);
                    break;
                case LevelUpItem.Rarity.Legendary:
                    AddItemToPool(item, legendaryItemPool);
                    break;
            }
        }
    }

    private void AddItemToPool(LevelUpItem item, Dictionary<LevelUpItem, int> pool)
    {
        if (pool.ContainsKey(item))
        {
            pool[item] += item.poolCount;
        }
        else
        {
            pool[item] = item.poolCount;
        }
    }

    public List<LevelUpItem> GetRandomItems(int itemCount)
    {
        int numItemsToGet = Mathf.Min(Count, itemCount);
        if (numItemsToGet <= 0) return null;

        HashSet<LevelUpItem> selectedItems = new();

        while (selectedItems.Count < numItemsToGet)
        {
            LevelUpItem item = GetRandomItem();

            if (item == null)
                break;

            selectedItems.Add(item);
        }

        return selectedItems.ToList();
    }

    public LevelUpItem GetRandomItem()
    {
        int roll = Random.Range(0, 100);

        if (roll >= legendaryThreshold && legendaryItemPool.Count > 0)
            return GetRandomItem(legendaryItemPool);
        
        if (roll >= rareThreshold && rareItemPool.Count > 0)
        return GetRandomItem(rareItemPool);
        
        if (roll >= uncommonThreshold && uncommonItemPool.Count > 0)
            return GetRandomItem(uncommonItemPool);
        
        if (commonItemPool.Count > 0)
            return GetRandomItem(commonItemPool);

        // At this point, the rolled rarity wasn't available.
        // Find any non-empty pool.
        if (uncommonItemPool.Count > 0)
            return GetRandomItem(uncommonItemPool);

        if (rareItemPool.Count > 0)
            return GetRandomItem(rareItemPool);

        if (legendaryItemPool.Count > 0)
            return GetRandomItem(legendaryItemPool);

        return null;
    }

    private LevelUpItem GetRandomItem(Dictionary<LevelUpItem, int> pool)
    {
        int index = Random.Range(0, pool.Keys.Count);
        return pool.Keys.ElementAt(index);
    }

    public void ItemSelected(LevelUpItem item)
    {
        switch (item.rarity)
            {
                case LevelUpItem.Rarity.Common:
                    DecrementItemPool(item, commonItemPool);
                    break;
                case LevelUpItem.Rarity.Uncommon:
                    DecrementItemPool(item, uncommonItemPool);
                    break;
                case LevelUpItem.Rarity.Rare:
                    DecrementItemPool(item, rareItemPool);
                    break;
                case LevelUpItem.Rarity.Legendary:
                    DecrementItemPool(item, legendaryItemPool);
                    break;
            }
    }

    private void DecrementItemPool(LevelUpItem item, Dictionary<LevelUpItem, int> pool)
    {
        if (!pool.ContainsKey(item)) return;

        pool[item]--;
        if (pool[item] <= 0)
            pool.Remove(item);
    }
}
