using System.Collections.Generic;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelUpItemPrefab;
    [SerializeField] private GameObject levelUpMenuPanel;
    [SerializeField] private int numItemsOffered = 3;
    [SerializeField] private ItemPool itemPool;
    private List<LevelUpItemUI> levelUpItemPanels = new();
    private PauseController _pauseController;
    private Player _player;
    private bool isInitialized = false;
    private bool isSubscribed = false;
    private int _openRequests;

    public void Initialize(Player player, PauseController pauseController)
    {
        _player = player;
        _pauseController = pauseController;
        isInitialized = true;
        TrySubscribe();
        levelUpMenuPanel.SetActive(false);
        InstantiateLevelUpItemPanels();
    }

    void OnEnable()
    {
        TrySubscribe();
    }

    void OnDisable()
    {
        TryUnsubscribe();
    }

    private bool TrySubscribe()
    {
        if (isSubscribed || !isInitialized || !isActiveAndEnabled) return false;

        _player.ExperienceController.LevelledUp += HandlePlayerLevelledUp;

        isSubscribed = true;

        return true;
    }

    private bool TryUnsubscribe()
    {
        if (!isSubscribed) return false;

        _player.ExperienceController.LevelledUp -= HandlePlayerLevelledUp;

        isSubscribed = false;

        return true;
    }

    private void HandlePlayerLevelledUp(int newLevel)
    {
        RequestOpen();
    }

    private void RequestOpen()
    {
        _openRequests++;

        // Open menu and randomize content if first request
        if (_openRequests == 1)
        {
            levelUpMenuPanel.SetActive(true);
            _pauseController.RequestPause();
            RandomizeLevelUpItemsOffered();
        }
    }

    private void ReleaseOpen()
    {
        _openRequests--;

        // If no more requests then close the menu
        if (_openRequests <= 0)
        {
            levelUpMenuPanel.SetActive(false);
            _pauseController.ReleasePause();
        }
        else // Else keep the menu open and re-ranzomize
        {
            RandomizeLevelUpItemsOffered();
        }
    }

    private void InstantiateLevelUpItemPanels()
    {
        numItemsOffered = Mathf.Min(numItemsOffered, itemPool.Count);

        for (int i=0; i<numItemsOffered; ++i)
        {
            LevelUpItemUI panel = Instantiate(levelUpItemPrefab, levelUpMenuPanel.transform).GetComponent<LevelUpItemUI>();
            levelUpItemPanels.Add(panel);
        }
    }

    private void RandomizeLevelUpItemsOffered()
    {
        CheckRemainingLootPoolSize();
        
        List<LevelUpItem> randomItems = itemPool.GetRandomItems(levelUpItemPanels.Count);
        for (int i=0; i<randomItems.Count; i++)
        {
            LevelUpItem item = randomItems[i];
            levelUpItemPanels[i].SetContent(item, () => SelectLevelUpItem(item));
        }
    }

    private void CheckRemainingLootPoolSize()
    {
        numItemsOffered = Mathf.Min(numItemsOffered, itemPool.Count);

        if (levelUpItemPanels.Count != numItemsOffered)
        {
            foreach (var panel in levelUpItemPanels)
            {
                Destroy(panel.gameObject);
            }
            levelUpItemPanels.Clear();
            InstantiateLevelUpItemPanels();
        }
    }

    private void SelectLevelUpItem(LevelUpItem item)
    {
        _player.InventoryController.AddLevelUpItem(item);
        itemPool.ItemSelected(item);
        ReleaseOpen();
    }
}
