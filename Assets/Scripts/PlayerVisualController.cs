using System.Collections;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Coroutine _pulseCoroutine;
    private float _currentOpacity = 1f;
    private float _invincibilityOpacity = 0.7f;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplySuperSpeedEffects() {
        _spriteRenderer.color = new Color(Color.red.r, Color.red.g, Color.red.b, _currentOpacity);
    }

    public void RevokeSuperSpeedEffects() {

    }

    public void ApplyInvincibilityEffects() {
        _currentOpacity = _invincibilityOpacity;
        _spriteRenderer.color = new Color(255, 255, 255, _invincibilityOpacity);
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
