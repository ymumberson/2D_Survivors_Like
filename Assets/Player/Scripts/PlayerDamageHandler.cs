using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof (HealthController))]
public class PlayerDamageHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.25f;
    [SerializeField] private Color damageFlash = Color.red;
    [SerializeField] private Color reviveFlash = Color.yellow;
    private HealthController _healthController;
    private Coroutine flashCoroutine;

    void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    void OnEnable()
    {
        _healthController.Damaged += OnPlayerDamaged;
        _healthController.Died += OnPlayerDied;
        _healthController.Revived += OnPlayerRevived;
    }

    void OnDisable()
    {
        _healthController.Damaged -= OnPlayerDamaged;
        _healthController.Died -= OnPlayerDied;
        _healthController.Revived -= OnPlayerRevived;
    }

    private void OnPlayerDamaged(float damageAmount)
    {
        if (_healthController.IsDead) return;
        
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        StartCoroutine(Flash(damageFlash));
    }

    private void OnPlayerRevived()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        spriteRenderer.color = Color.white;
        StartCoroutine(Flash(reviveFlash));
    }

    private void OnPlayerDied()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        spriteRenderer.color = Color.black;
    }

    private IEnumerator Flash(Color flashColor)
    {
        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;

        float halfDuration = flashDuration / 2f;
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / halfDuration;
            spriteRenderer.color = Color.Lerp(originalColor, flashColor, t);
            
            yield return null;
        }

        elapsed = 0;

        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / halfDuration;
            spriteRenderer.color = Color.Lerp(flashColor, originalColor, t);
            
            yield return null;
        }

        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }
}
