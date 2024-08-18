using UnityEngine;

public abstract class PlatformMovementScript : MonoBehaviour
{
    protected Rigidbody2D _rb;
    [SerializeField] protected float _movementSpeed = 3f;

    public Vector2 Velocity {
        get {
            return _rb.velocity;
        }
    }
    
    protected virtual void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Move();
    }

    protected abstract void Move();
}
