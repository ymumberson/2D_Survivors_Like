using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgrade", menuName = "Level Up/Weapon/Weapon Upgrade")]
public class WeaponUpgrade : LevelUpItem
{
    [SerializeField] private GameObject weaponPrefab;
    
    public override void Apply(Player player)
    {
        player.WeaponController.AddWeapon(weaponPrefab);
    }

    public override void Remove(Player player)
    {
        player.WeaponController.RemoveWeapon(weaponPrefab);
    }
}
