using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponSoundController : MonoBehaviour
{
    [SerializeField] private SoundEffect attackSound;
    private Weapon _weapon;

    void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }

    void OnEnable()
    {
        _weapon.Attacked += HandleAttacked;
    }

    void OnDisable()
    {
        _weapon.Attacked -= HandleAttacked;
    }

    private void HandleAttacked(HitController.HitType hitType)
    {
        if (hitType != HitController.HitType.Initial) return;

        AudioManager.Instance.PlaySFX(attackSound);
    }
}
