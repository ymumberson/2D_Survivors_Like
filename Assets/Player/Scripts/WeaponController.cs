using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private AttackController attackController;
    [SerializeField] private GameObject startingWeapon;
    private Dictionary<GameObject, Weapon> weapons = new();
    private const float DAMAGE_INCREASE = 5;

    void Start()
    {
        AddWeapon(startingWeapon);
    }

    public void AddWeapon(GameObject weaponPrefab)
    {
        if (weapons.ContainsKey(weaponPrefab))
        {
            ModifyWeaponStats(weapons[weaponPrefab]);
        }
        else
        {
            InstantiateWeapon(weaponPrefab);
        }
    }

    public void RemoveWeapon(GameObject weaponPrefab)
    {
        if (weapons.ContainsKey(weaponPrefab))
        {
            ModifyWeaponStats(weapons[weaponPrefab]);
        }
        else
        {
            DestroyWeapon(weaponPrefab);
        }
    }

    private void ModifyWeaponStats(Weapon weapon)
    {
        
    }

    private void InstantiateWeapon(GameObject weaponPrefab)
    {
        GameObject weaponGO = Instantiate(weaponPrefab, transform);
        Weapon weapon = weaponGO.GetComponent<Weapon>();
        weapon.Initialize(attackController);
        weapons.Add(weaponPrefab, weapon);
    }

    private void DestroyWeapon(GameObject weaponPrefab)
    {
        Destroy(weapons[weaponPrefab].gameObject);
        weapons.Remove(weaponPrefab);
    }
}
