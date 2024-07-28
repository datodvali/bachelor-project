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

    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 12f;
    [SerializeField] private float _jumpInitialSpeed = 10f;
    private float _superSpeedTimeRemaining = 0f;
    private readonly float _superSpeedMultiplier = 1.4f;
    private bool _superSpeedEffectPulsing = false;
    public bool hasSecondLife = false;
    private bool _hasSuperSpeed = false;
    private bool _facingRight = true;
    private bool _isMoving;
    private bool _isRunning;
    private Vector2 _moveInput;

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
            _isRunning = value;
            _animator.SetBool(AnimationNames.isRunning, value);
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

    private float CurrentSpeed {
        get {
            if (!IsMoving || _touchDirections.IsOnWall) return 0;
            float multiplier = _hasSuperSpeed ? _superSpeedMultiplier : 1f;
            if (IsMoving && IsRunning) return _runSpeed * multiplier;
            return _walkSpeed * multiplier;
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
            _rigidBody.velocity = new Vector2(_moveInput.x * CurrentSpeed, _rigidBody.velocity.y);
        }
        _animator.SetFloat(AnimationNames.yVelocity, _rigidBody.velocity.y);
    }

    void Update() {
        CheckSuperSpeed();
    }

    public void OnMove(InputAction.CallbackContext context) {
        _moveInput = context.ReadValue<Vector2>();
        IsMoving = _moveInput != Vector2.zero;
        ChangeDirection(_moveInput);
    }

    public void OnRun(InputAction.CallbackContext context) {
        if (context.started) {
            IsRunning = true;
        } else if (context.canceled) {
            IsRunning = false;            
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.started && _touchDirections.IsOnGround) {
            _animator.SetTrigger(AnimationNames.jump);
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpInitialSpeed);
        }
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

    private void ChangeDirection(Vector2 moveInput) {
        if (moveInput.x > 0 && !_facingRight) {
            FacingRight = true;
        } else if (moveInput.x < 0 && _facingRight) {
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
}
