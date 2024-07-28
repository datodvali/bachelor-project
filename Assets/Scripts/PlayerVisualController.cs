using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Coroutine _pulseCoroutine;
    private VisualState _defaultState = new(Color.white, 1f);
    private VisualState _currentState = new(Color.white, 1f);
    private float _invincibilityOpacity = 0.7f;
    private Color _superSpeedColor = Color.red;
    private int _pulsingEffects = 0;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplySuperSpeedEffects() {
        _currentState.color = _superSpeedColor;
        ApplyColorAndOpacity();
    }

    public void RevokeSuperSpeedEffects() {
        StopPulsing();
        _currentState.color = _defaultState.color;
        ApplyColorAndOpacity();
    }

    public void ApplyInvincibilityEffects() {
        _currentState.opacity = _invincibilityOpacity;
        ApplyColorAndOpacity();
    }

    public void RevokeInvincibilityEffects() {
        StopPulsing();
        _currentState.opacity = _defaultState.opacity;
        ApplyColorAndOpacity();
    }

    public void StartPulsing() {
        _pulsingEffects++;
        _pulseCoroutine ??= StartCoroutine(Pulse());
    }

    public void StopPulsing() {
        _pulsingEffects--;
        Debug.Log(_pulsingEffects);
        if (_pulsingEffects == 0 && _pulseCoroutine != null) {
            StopCoroutine(_pulseCoroutine);
            _pulseCoroutine = null;
        }
    }

    private void ApplyColorAndOpacity() {
        Color currColor = _currentState.color;
        float currOpacity = _currentState.opacity;
        _spriteRenderer.color = new Color(currColor.r, currColor.g, currColor.b, currOpacity);
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

    private class VisualState {
        internal Color color;
        internal float  opacity;
        internal VisualState(Color color, float opacity) {
            this.color = color;
            this.opacity = opacity;
        }
    }
}
