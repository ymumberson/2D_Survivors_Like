using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgrade", menuName = "Level Up/Weapon/Weapon Upgrade")]
public class WeaponUpgrade : LevelUpItem
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField, Tooltip("Stat increases if the weapon is already owned")]
    private Weapon.WeaponStats additionalStats;
    
    public override void Apply(Player player)
    {
        player.WeaponController.AddWeapon(weaponPrefab, additionalStats);
    }

    public override void Remove(Player player)
    {
        player.WeaponController.RemoveWeapon(weaponPrefab, additionalStats);
    }
}
