using System.Collections;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Coroutine _pulseCoroutine;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplySuperSpeedEffects() {
        _spriteRenderer.color = Color.red;
    }

    public void ApplyInvincibilityEffects() {
        _spriteRenderer.color = new Color(255, 255, 255, 0.7f);
    }

    public void ApplyNormalEffects() {
        StopPulsing();
        _spriteRenderer.color = Color.white;
    }

    public void ApplyPulsingEffects() {
        StartPulsing();
    }

    private void StartPulsing() {
        if (_pulseCoroutine == null) {
            _pulseCoroutine = StartCoroutine(Pulse());
        }
    }

    private void StopPulsing() {
        if (_pulseCoroutine != null) {
            StopCoroutine(_pulseCoroutine);
            _pulseCoroutine = null;
        }
    }
    
    private IEnumerator Pulse() {
        float pulseDuration = 0.3f;
        float minAlpha = 0.2f;
        float maxAlpha = 1.0f;
        while (true) {
            // Fade out
            for (float t = 0; t < pulseDuration; t += Time.deltaTime) {
                float normalizedTime = t / pulseDuration;
                Color color = _spriteRenderer.color;
                color.a = Mathf.Lerp(maxAlpha, minAlpha, normalizedTime);
                _spriteRenderer.color = color;
                yield return null;
            }
            // Fade in
            for (float t = 0; t < pulseDuration; t += Time.deltaTime) {
                float normalizedTime = t / pulseDuration;
                Color color = _spriteRenderer.color;
                color.a = Mathf.Lerp(minAlpha, maxAlpha, normalizedTime);
                _spriteRenderer.color = color;
                yield return null;
            }
        }
    }
}
