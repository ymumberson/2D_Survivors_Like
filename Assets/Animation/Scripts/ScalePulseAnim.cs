using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScalePulseAnim : MonoBehaviour
{
    [SerializeField] private float scaleToPercentage = 0.975f;
    [SerializeField] private ScaleDirection scaleDirection = ScaleDirection.Both;
    [SerializeField] private float pulseDuration = 1f;

    private SpriteRenderer _spriteRenderer;
    private Vector3 _originalScale;

    public enum ScaleDirection
    {
        Both = 1,
        OnlyX = 2,
        OnlyY = 3,
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalScale = transform.localScale;
    }

    void OnEnable()
    {
        StartCoroutine(PulseScale());
    }

    void OnDisable()
    {
        transform.localScale = _originalScale;
    }

    private IEnumerator PulseScale()
    {
        float halfPulseDuration = pulseDuration / 2;
        float interval = 0;

        Vector3 targetScale;
        switch (scaleDirection)
        {
            case ScaleDirection.OnlyX:
                targetScale = new Vector3(_originalScale.x * scaleToPercentage, _originalScale.y, _originalScale.z);
                break;
            case ScaleDirection.OnlyY:
            targetScale = new Vector3(_originalScale.x, _originalScale.y * scaleToPercentage, _originalScale.z);
                break;
            case ScaleDirection.Both:
            default:
                targetScale = _originalScale * scaleToPercentage;
                break;
        }
        
        while (true)
        {
            while (interval < halfPulseDuration)
            {
                interval += Time.deltaTime;
                float t = interval / halfPulseDuration;
                transform.localScale = Vector3.Lerp(_originalScale, targetScale, t);
                yield return null;
            }

            transform.localScale = targetScale;
            interval = 0;

            while (interval < halfPulseDuration)
            {
                interval += Time.deltaTime;
                float t = interval / halfPulseDuration;
                transform.localScale = Vector3.Lerp(targetScale, _originalScale, t);
                yield return null;
            }

            transform.localScale = _originalScale;
            interval = 0;
        }
    }
}
