using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject startingWeapon;
    private Dictionary<GameObject, Weapon> weapons = new();
    private Character _character;

    public void Initialise(Character character)
    {
        _character = character;
        AddWeapon(startingWeapon, new WeaponStats());
    }

    public void AddWeapon(GameObject weaponPrefab, WeaponStats statsIncrease)
    {
        if (weapons.ContainsKey(weaponPrefab))
        {
            weapons[weaponPrefab].IncreaseStats(statsIncrease);
        }
        else
        {
            InstantiateWeapon(weaponPrefab);
        }
    }

    public void RemoveWeapon(GameObject weaponPrefab, WeaponStats statsDecrease)
    {
        if (weapons.ContainsKey(weaponPrefab))
        {
            weapons[weaponPrefab].DecreaseStats(statsDecrease);
        }
        else
        {
            DestroyWeapon(weaponPrefab);
        }
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
