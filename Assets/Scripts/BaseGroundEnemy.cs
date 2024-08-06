using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchDirections), typeof(Animator))]
public class BaseGroundEnemy : MonoBehaviour {
    [SerializeField] protected float _runSpeed = 4f;
    private Rigidbody2D _rigidBody;
    private TouchDirections _touchDirections;
    [SerializeField] private DetectionZone _attackZone;
    [SerializeField] private DetectionZone _groundZone;
    private Animator _animator;
    private Damageable _damageable;

    public enum Direction { RIGHT, LEFT }
    private Direction _moveDirection = Direction.RIGHT;
    private bool _hasTarget = false;

    protected virtual float CurrentSpeed
    {
        get
        {
            if (!_damageable.IsAlive) return 0;
            return _runSpeed;
        }
    }

    public Direction MoveDirection
    {
        get => _moveDirection;
        private set
        {
            if (_moveDirection != value)
            {
                _moveDirection = value;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
        }
    }

    public bool HasTarget
    {
        get => _hasTarget;
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationNames.hasTarget, value);
        }
    }

    public bool LockVelocity
    {
        get => _animator.GetBool(AnimationNames.lockVelocity);
        set => _animator.SetBool(AnimationNames.lockVelocity, value);
    }

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _touchDirections = GetComponent<TouchDirections>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    protected virtual void Start()
    {
        _animator.SetBool(AnimationNames.isMoving, true);
    }

    protected virtual void Update()
    {
        HasTarget = _attackZone.detectedColliders.Count > 0;
    }

    protected virtual void FixedUpdate()
    {
        if (LockVelocity)
        {
            _rigidBody.velocity = Vector2.zero;
            return;
        }
        if (_touchDirections.IsOnGround && _touchDirections.IsOnWall)
        {
            ChangeDirection();
        }
        float moveSpeed = CurrentSpeed * (MoveDirection == Direction.RIGHT ? 1 : -1);
        _rigidBody.velocity = new Vector2(moveSpeed, _rigidBody.velocity.y);
    }

    protected virtual void ChangeDirection()
    {
        MoveDirection = (MoveDirection == Direction.RIGHT) ? Direction.LEFT : Direction.RIGHT;
    }

    public virtual void OnDamageTaken(int damage, Vector2 knockBack)
    {
        _rigidBody.velocity = new Vector2(knockBack.x, _rigidBody.velocity.y + knockBack.y);
    }

    public virtual void CliffDetected()
    {
        if (_touchDirections.IsOnGround)
        {
            ChangeDirection();
        }
    }
}