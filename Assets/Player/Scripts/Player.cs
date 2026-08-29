using UnityEngine;

public class Player : Character
{
    [SerializeField] WeaponController weaponController;

    public WeaponController WeaponController => weaponController;
}
