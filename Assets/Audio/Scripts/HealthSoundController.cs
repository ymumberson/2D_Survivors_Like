using System;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class HealthSoundController : MonoBehaviour
{
    [SerializeField] private SoundEffect damagedSound;
    [SerializeField] private SoundEffect healedSound;
    [SerializeField] private SoundEffect diedSound;
    private HealthController _healthController;

    void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    void OnEnable()
    {
        _healthController.Damaged += HandleDamaged;
        _healthController.Healed += HandleHealed;
        _healthController.Died += HandleDied;
    }

    void OnDisable()
    {
        _healthController.Damaged -= HandleDamaged;
        _healthController.Healed -= HandleHealed;
        _healthController.Died -= HandleDied;
    }

    private void HandleDamaged(float amountDamaged)
    {
        PlaySound(damagedSound);
    }

    private void HandleHealed(float amountHealed)
    {
        PlaySound(healedSound);
    }

    private void HandleDied()
    {
        PlaySound(diedSound);
    }

    private void PlaySound(SoundEffect sound)
    {
        if (!sound || !AudioManager.Instance) return;

        AudioManager.Instance.PlaySFX(sound);
    }
}
