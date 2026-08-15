using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class HealthGraphicHandler : MonoBehaviour
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private float flashDuration = 0.25f;
    [SerializeField] private Color damageFlash = Color.red;
    [SerializeField] private Color reviveFlash = Color.yellow;
    [SerializeField] private Color deathColor = Color.black;
    [SerializeField] private Color healColor = Color.green;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private Coroutine flashCoroutine;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    void OnEnable()
    {
        healthController.Damaged += OnDamaged;
        healthController.Died += OnDied;
        healthController.Revived += OnRevived;
        healthController.Healed += OnHealed;
    }

    void OnDisable()
    {
        healthController.Damaged -= OnDamaged;
        healthController.Died -= OnDied;
        healthController.Revived -= OnRevived;
        healthController.Healed -= OnHealed;
    }

    private void OnDamaged(float damageAmount)
    {
        if (healthController.IsDead) return;
        
        FlashColor(damageFlash);
    }

    private void OnRevived()
    {
        FlashColor(reviveFlash);
    }

    private void OnDied()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        _spriteRenderer.color = deathColor;
    }

    private void OnHealed(float healAmount)
    {
        FlashColor(healColor);
    }

    private void FlashColor(Color flashColor)
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        StartCoroutine(Flash(flashColor));
    }

    private IEnumerator Flash(Color flashColor)
    {
        float elapsed = 0f;

        float halfDuration = flashDuration / 2f;
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / halfDuration;
            _spriteRenderer.color = Color.Lerp(_originalColor, flashColor, t);
            
            yield return null;
        }

        elapsed = 0;

        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / halfDuration;
            _spriteRenderer.color = Color.Lerp(flashColor, _originalColor, t);
            
            yield return null;
        }

        _spriteRenderer.color = _originalColor;
        flashCoroutine = null;
    }
}
