using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private Vector2 _knockBack = new(2f, 0);
    [SerializeField] private Vector2 _moveVelocity = new(4f, 0);
    
    private Rigidbody2D _rb;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.velocity = new Vector2(_moveVelocity.x * transform.localScale.x, _moveVelocity.y);
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var damageable)) {
            Vector2 kb = transform.localScale.x > 0 ? _knockBack : new Vector2(_knockBack.x * -1, _knockBack.y);
            damageable.OnHit(_damage, kb);
            Destroy(gameObject);
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground")
        || collider.gameObject.layer == LayerMask.NameToLayer("Platform") 
        || collider.gameObject.layer == LayerMask.NameToLayer("Trap")) {
            Destroy(gameObject);
        }
    }
}
