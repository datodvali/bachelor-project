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
    private PlatformMovementScript _platform;

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
        _runSpeed *= transform.localScale.x;
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
        _rigidBody.velocity = new Vector2(CurrXVelocity, _rigidBody.velocity.y);
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