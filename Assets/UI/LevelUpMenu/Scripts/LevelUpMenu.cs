using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelUpItemPrefab;
    [SerializeField] private GameObject levelUpMenuPanel;
    [SerializeField] private int numItemsOffered = 3;
    [SerializeField] private List<LevelUpItem> levelUpItemPool = new();
    private List<LevelUpItemUI> levelUpItemPanels = new();
    private PauseController _pauseController;
    private Player _player;
    private bool isInitialized = false;
    private bool isSubscribed = false;

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
        RandomizeLevelUpItemsOffered();
        levelUpMenuPanel.SetActive(true);
        _pauseController.RequestPause();
    }

    private void InstantiateLevelUpItemPanels()
    {
        numItemsOffered = Mathf.Min(numItemsOffered, levelUpItemPool.Count);

        for (int i=0; i<numItemsOffered; ++i)
        {
            LevelUpItemUI panel = Instantiate(levelUpItemPrefab, levelUpMenuPanel.transform).GetComponent<LevelUpItemUI>();
            levelUpItemPanels.Add(panel);
        }
    }

    private void RandomizeLevelUpItemsOffered()
    {
        List<LevelUpItem> pool = levelUpItemPool.FindAll((item) => item != null);
        foreach (LevelUpItemUI panel in levelUpItemPanels)
        {
            LevelUpItem item = SelectRandomLevelUpItem(pool);
            pool.Remove(item);
            panel.SetContent(item, () => SelectLevelUpItem(item));
        }
    }

    private LevelUpItem SelectRandomLevelUpItem(List<LevelUpItem> pool)
    {
        int randomIndex = Random.Range(0, pool.Count);
        return pool[randomIndex];
    }

    private void SelectLevelUpItem(LevelUpItem item)
    {
        _player.InventoryController.AddLevelUpItem(item);
        levelUpMenuPanel.SetActive(false);
        _pauseController.ReleasePause();
    }
}
