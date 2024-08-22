using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Color _startingColor;
    [SerializeField] private float _postCollisionHoverTime;
    [SerializeField] private float _maxFallTime;
    private float _timeAfterCollision;
    private float _timeInFall;
    private bool _collided;
    private bool _falling;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startingColor = _spriteRenderer.color;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        _collided = true;
    }

    void Update() {
        if (!_collided) return;
        if (_falling) {
            _timeInFall += Time.deltaTime;
            if (_timeInFall > _maxFallTime) Destroy(gameObject);
            UpdateTransparency();
        } else {
            _timeAfterCollision += Time.deltaTime;
            if (_timeAfterCollision > _postCollisionHoverTime) Fall();
        }
    }

    private void Fall() {
        _falling = true;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = 5;
    }

    private void UpdateTransparency() {
        _spriteRenderer.color = new(_startingColor.r, _startingColor.g, _startingColor.b, _startingColor.a * (1f - _timeInFall / _maxFallTime));
    }
}
