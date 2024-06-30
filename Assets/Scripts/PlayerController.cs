using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(TouchDirections))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private TouchDirections _touchDirections;

    [SerializeField] private float walkSpeed = 5f; 
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float jumpInitialSpeed = 10f;
    
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
            if (IsMoving && IsRunning) return runSpeed;
            return walkSpeed;
        }
    }

    void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchDirections = GetComponent<TouchDirections>();
    }

    void FixedUpdate() {
        if (!LockVelocity) {
            _rigidBody.velocity = new Vector2(_moveInput.x * CurrentSpeed, _rigidBody.velocity.y);
        }
        _animator.SetFloat(AnimationNames.yVelocity, _rigidBody.velocity.y);
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
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpInitialSpeed);
        }
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.started) {
            _animator.SetTrigger(AnimationNames.attack);
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
        Debug.Log("damage " + damage);
    }
}
