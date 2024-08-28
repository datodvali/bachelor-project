using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchDirections), typeof(Animator))]
public class KnightLogic : MonoBehaviour
{

    [SerializeField] private float runSpeed = 5f;
    private Rigidbody2D _rigidBody;
    private TouchDirections _touchDirections;
    [SerializeField] private DetectionZone _attackZone;
    [SerializeField] private DetectionZone _groundZone;
    private Animator _animator;
    private Damageable _damageable;

    public enum Direction {RIGHT, LEFT}
    private Direction _moveDirection = Direction.RIGHT;
    private bool _hasTarget = false;

    private float CurrentSpeed {
        get {
            if (!_damageable.IsAlive) return 0;
            return runSpeed;
        }
    }

    public Direction MoveDirection {
        get {
            return _moveDirection;
        }
        private set {
            if (value != _moveDirection)
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            _moveDirection = value;
        }
    }

    public bool HasTarget {
        get {
            return _hasTarget;
        }
        private set {
            _hasTarget = value;
            _animator.SetBool(AnimationNames.hasTarget, value);
        }
    }

    public bool LockVelocity {
        get {
            return _animator.GetBool(AnimationNames.lockVelocity);
        }
        set {
            _animator.SetBool(AnimationNames.lockVelocity, value);
        }
    }

    void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _touchDirections = GetComponent<TouchDirections>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    void Start() {
        _animator.SetBool(AnimationNames.isMoving, true);
    }

    void Update() {
        HasTarget = _attackZone.detectedColliders.Count > 0;
    }

    void FixedUpdate() {
        if (LockVelocity) {
            _rigidBody.velocity = Vector2.zero;
            return;
        }
        if (_touchDirections.IsOnGround && _touchDirections.IsOnWall) {
            ChangeDirection();
        }
        _rigidBody.velocity = new Vector2(CurrentSpeed * (MoveDirection == Direction.RIGHT ? 1 : -1),_rigidBody.velocity.y);
    }

    private void ChangeDirection() {
        if (MoveDirection == Direction.RIGHT) {
            MoveDirection = Direction.LEFT;
        } else if (MoveDirection == Direction.LEFT) {
            MoveDirection = Direction.RIGHT;
        } else {
            Debug.LogError("Knight move direction is not set to any legal value");
        }
    }

    public void OnDamageTaken(int damage, Vector2 knockBack) {
        _rigidBody.velocity = new Vector2(knockBack.x, _rigidBody.velocity.y + knockBack.y);
    }

    public void CliffDetected() {
        if (_touchDirections.IsOnGround) {
            ChangeDirection();
        }
    }
}
