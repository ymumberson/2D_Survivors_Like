using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject startingWeapon;
    private Dictionary<GameObject, Weapon> weapons = new();
    private Character _character;

    public void Initialise(Character character)
    {
        _character = character;
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
        //TODO: Allow modifying a weapon's base stats
    }

    private void InstantiateWeapon(GameObject weaponPrefab)
    {
        GameObject weaponGO = Instantiate(weaponPrefab, transform);
        Weapon weapon = weaponGO.GetComponent<Weapon>();
        weapon.Initialize(_character);
        weapons.Add(weaponPrefab, weapon);
    }

    private void DestroyWeapon(GameObject weaponPrefab)
    {
        Destroy(weapons[weaponPrefab].gameObject);
        weapons.Remove(weaponPrefab);
    }
}
