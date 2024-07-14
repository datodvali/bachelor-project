using TMPro;
using UnityEngine;

public class HealthTextScript : MonoBehaviour
{
    private RectTransform _rectTransform;
    private TextMeshProUGUI _textMeshPro;
    private Vector3 _moveVector = Vector3.up * 150;
    private float _timeToFade = 1f;
    private float _elapsedTime = 0f;
    private Color _initialColor;


    void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _initialColor = _textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        _rectTransform.position += _moveVector * Time.deltaTime;
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime < _timeToFade) {
            _textMeshPro.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, _initialColor.a * (1 - _elapsedTime / _timeToFade));
        } else {
            Destroy(gameObject);
        }
    }
}
