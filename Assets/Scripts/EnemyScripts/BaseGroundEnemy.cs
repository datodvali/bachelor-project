using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchDirections), typeof(Animator))]
public class BaseGroundEnemy : MonoBehaviour {
    [SerializeField] protected float _runSpeed = 4f;
    protected Rigidbody2D _rigidBody;
    protected TouchDirections _touchDirections;
    [SerializeField] protected DetectionZone _attackZone;
    [SerializeField] protected DetectionZone _groundZone;
    protected Animator _animator;
    protected Damageable _damageable;

    public enum Direction { RIGHT, LEFT }
    protected Direction _moveDirection = Direction.RIGHT;
    protected bool _hasTarget = false;
    protected PlatformMovementScript _platform;

    protected virtual float CurrXVelocity
    {
        get
        {
            if (!_damageable.IsAlive) return 0;
            float currXVelocity = _runSpeed * (MoveDirection == Direction.RIGHT ? 1 : -1);
            if (_touchDirections.IsOnPlatform && _platform != null) currXVelocity += _platform.Velocity.x;
            return currXVelocity;
        }
    }

    public Direction MoveDirection
    {
        get => _moveDirection;
        protected set
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
        protected set
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

    void Start()
    {
        _runSpeed *= transform.localScale.x;
        _animator.SetBool(AnimationNames.isMoving, true);
    }

    void Update()
    {
        HasTarget = _attackZone.detectedColliders.Count > 0;
        UpdateImpl();
    }

    protected virtual void UpdateImpl() {
    }

    void FixedUpdate()
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
        _rigidBody.velocity = new Vector2(CurrXVelocity, _rigidBody.velocity.y);
        FixedUpdateImpl();
    }

    protected virtual void FixedUpdateImpl() {
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

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent<PlatformMovementScript>(out var platform)) {
            _platform = platform;
        }
    }

    public void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent<PlatformMovementScript>(out var platform)) {
            _platform = null;
        }
    }
}