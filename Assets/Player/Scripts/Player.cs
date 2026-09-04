using UnityEngine;

public class Player : Character
{
    [SerializeField] WeaponController weaponController;
    [SerializeField] OnHitController onHitController;

    public WeaponController WeaponController => weaponController;
    public OnHitController OnHitController => onHitController;
}
