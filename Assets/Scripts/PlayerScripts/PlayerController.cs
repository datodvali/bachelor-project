using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(TouchDirections))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private TouchDirections _touchDirections;
    private Damageable _damageable;
    private PlayerVisualController _playerVisualController;
    [SerializeField] private ParticleSystem _dustPS;

    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 12f;
    [SerializeField] private float _jumpInitialSpeed = 10f;
    [SerializeField] private float _climbSpeed = 4f;
    private float _superSpeedTimeRemaining = 0f;
    private readonly float _superSpeedMultiplier = 1.3f;
    private bool _superSpeedEffectPulsing = false;
    public bool hasSecondLife = false;
    private bool _hasSuperSpeed = false;
    private bool _facingRight = true;
    private bool _isMoving;
    private bool _isRunning;
    private bool _isClimbing;
    private bool _isOnWallHang;
    private bool _readyForRun;
    private readonly float _coyoteTime = 0.1f;
    private float _timeInAir = 0f; 
    private readonly float _jumpBufferDuration = 0.2f;
    private float _jumpBufferTimer = 0f;
    private bool _jumped = false;
    private Vector2 _moveInput;
    private PlatformMovementScript _platform;
    private readonly string _layerToCheck = "Platform";

    public bool IsMoving {
        get {
            return _isMoving;
        }
        private set {
            _isMoving = value;
            _animator.SetBool(AnimationNames.isMoving, value);
        }
    }

    public bool IsRunning {
        get {
            return _isRunning;
        }
        private set {
            if (value && !_isRunning) CreateDust();
            _isRunning = value;
            _animator.SetBool(AnimationNames.isRunning, value);
        }
    }

    public bool IsClimbing {
        get {
            return _isClimbing;
        }
        private set {
            _isClimbing = value;
            _animator.SetBool(AnimationNames.isClimbing, value);
        }
    }

    public bool IsOnWallHang {
        get {
            return _isOnWallHang;
        }
        private set {
            _isOnWallHang = value;
            _animator.SetBool(AnimationNames.isOnWallHang, value);
        }
    }

    public bool FacingRight {
        get {
            return _facingRight;
        }
        private set {
            if (_facingRight != value) {
                transform.localScale *= new Vector2(-1, 1);
            }
            _facingRight = value;
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

    private float CurrentXVelocity {
        get {
            float currXVelocity;
            
            if (!IsMoving || _touchDirections.IsOnWall) currXVelocity = 0;
            else if (IsRunning) currXVelocity = _runSpeed;
            else currXVelocity = _walkSpeed;
            
            float multiplier = _hasSuperSpeed ? _superSpeedMultiplier : 1f;
            currXVelocity *= multiplier * _moveInput.x;
            
            if (_touchDirections.IsOnPlatform && _platform != null) currXVelocity += _platform.Velocity.x;
            return currXVelocity;
        }
    }

    private float CurrentYVelocity {
        get {
            if (IsClimbing) {
                return _climbSpeed * _moveInput.y;
            } else if (IsOnWallHang) {
                return 0;
            } else {
                return _rigidBody.velocity.y;
            }
        }
    }

    void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchDirections = GetComponent<TouchDirections>();
        _damageable = GetComponent<Damageable>();
        _playerVisualController = GetComponent<PlayerVisualController>();
    }

    void FixedUpdate() {
        if (!LockVelocity) {
            _rigidBody.velocity = new Vector2(CurrentXVelocity, CurrentYVelocity);
        }
        _animator.SetFloat(AnimationNames.yVelocity, _rigidBody.velocity.y);
    }

    void Update() {
        CheckCoyoteTime();
        CheckJumpBuffer();
        CheckSuperSpeed();
        CheckClimbing();
    }

    public void OnMove(InputAction.CallbackContext context) {
        _moveInput = context.ReadValue<Vector2>();
        IsMoving = _moveInput.x != 0;
        if (!IsOnWallHang) {
            ChangeDirection(_moveInput.x);
            if (IsMoving && _readyForRun) IsRunning = true;
        }
        else OnClimb(_moveInput.y);
    }

    private void OnClimb(float inputY) {
        if (inputY != 0 && IsOnWallHang) IsClimbing = true;
        else IsClimbing = false;
    }

    public void OnWallHang(InputAction.CallbackContext context) {
        if (context.started && _touchDirections.IsOnWall) {
            IsOnWallHang = true;
        } else if (context.canceled) {
            IsOnWallHang = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context) {
        if (context.started) {
            _readyForRun = true;
            if (IsMoving) IsRunning = true;
        } else if (context.canceled) {
            _readyForRun = false;
            IsRunning = false;          
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (!context.started) return;
        if (_touchDirections.IsOnGround || IsOnWallHang || IsCoyoteTime()) {
            Jump();
        } else {
            _jumpBufferTimer = _jumpBufferDuration;
        }
    }

    private void Jump() {
        if (IsOnWallHang) WallJump();
        if (_touchDirections.IsOnGround) CreateDust();
        _animator.SetTrigger(AnimationNames.jump);
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpInitialSpeed);
        _jumped = true;
        _jumpBufferTimer = 0;
    }

    private void WallJump() {
        if (IsClimbing) IsClimbing = false;
        if (IsOnWallHang) IsOnWallHang = false;
         _animator.SetTrigger(AnimationNames.jump);
        _rigidBody.velocity = new Vector2(_moveInput.x, _jumpInitialSpeed);
        _jumped = true;
        _jumpBufferTimer = 0;
    }

    private bool IsCoyoteTime() {
        return _timeInAir < _coyoteTime && !_jumped;
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.started) {
            _animator.SetTrigger(AnimationNames.attack);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context) {
        if (context.started) {
            _animator.SetTrigger(AnimationNames.rangedAttack);
        }
    }

    private void ChangeDirection(float inputX) {
        if (inputX > 0 && !_facingRight) {
            FacingRight = true;
        } else if (inputX < 0 && _facingRight) {
            FacingRight = false;
        }
    }

    public void OnDamageTaken(int damage, Vector2 knockBack) {
        LockVelocity = true;
        _rigidBody.velocity = new Vector2(knockBack.x, _rigidBody.velocity.y + knockBack.y);
    }

    public void OnSecondLifeGained() {
        if (hasSecondLife == true) return;
        hasSecondLife = true;
        CharacterEvents.secondLifeGained(gameObject);
    }

    public void OnSuperSpeedGained(float superSpeedTime) {
        _hasSuperSpeed = true;
        _superSpeedTimeRemaining = superSpeedTime;
        _playerVisualController.ApplySuperSpeedEffects();
    }

    private void OnDeath() {
        if (hasSecondLife) {
            hasSecondLife = false;
            _damageable.IsAlive = true;
            _damageable.Health = _damageable.MaxHealth;
        } else {
            GameEvents.gameOver.Invoke();
        }
    }

    public void CheckCoyoteTime() {
        if (_touchDirections.IsOnGround && _timeInAir > 0) {
            _timeInAir = 0;
            _jumped = false;
        } else if (!_touchDirections.IsOnGround) {
            _timeInAir += Time.deltaTime;
        }
    }

    public void CheckJumpBuffer() {
        if (!_touchDirections.IsOnGround && _jumpBufferTimer > 0) {
            _jumpBufferTimer -= Time.deltaTime;
        } else if (_touchDirections.IsOnGround && _jumpBufferTimer > 0) {
            Jump();
        }
    }

    private void CheckSuperSpeed() {
        if (!_hasSuperSpeed) return;
        _superSpeedTimeRemaining -= Time.deltaTime;
        if (_superSpeedTimeRemaining <= 0) {
            _superSpeedTimeRemaining = 0;
            _hasSuperSpeed = false;
            _playerVisualController.RevokeSuperSpeedEffects();
            _superSpeedEffectPulsing = false;
        } else {
            ApplyPulseEffectIfNecessary();
        }
    }

    private void CheckClimbing() {
        if (IsClimbing && !_touchDirections.IsOnWall) {
            IsClimbing = false;
        }
    }

    private void ApplyPulseEffectIfNecessary() {
        if (_superSpeedTimeRemaining <= 2f && !_superSpeedEffectPulsing) {
            _superSpeedEffectPulsing = true;
            _playerVisualController.StartPulsing();
        }
    }

    void OnEnable() {
        _damageable.deathEvent.AddListener(OnDeath);
    }

    void OnDisable() {
        _damageable.deathEvent.RemoveListener(OnDeath);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (LayerMask.NameToLayer(_layerToCheck) == collision.gameObject.layer) {
            
            if (collision.gameObject.TryGetComponent<PlatformMovementScript>(out var platform)) {
                _platform = platform;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision) {
        if (LayerMask.NameToLayer(_layerToCheck) == collision.gameObject.layer) {
            if (collision.gameObject.TryGetComponent<PlatformMovementScript>(out var platform)) {
                _platform = null;
            }
        }
    }

    public void CreateDust() {
        _dustPS.Play();
    }
}
