using UnityEngine;

public class BombScript : MonoBehaviour
{
    private Vector2 _moveVelocity = new(23f, 4f);
    private Animator _animator;
    private Rigidbody2D _rb;
    private Collider2D _collider;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _rb.velocity = new Vector2(_moveVelocity.x * transform.localScale.x, _moveVelocity.y);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.velocity = Vector2.zero;
        _collider.enabled = false;
        _animator.SetTrigger(AnimationNames.explosion);
    }

    public void DestroyBomb() {
        Destroy(gameObject);
    }
}
