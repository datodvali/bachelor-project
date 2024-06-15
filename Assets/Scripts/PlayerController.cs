using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(TouchDirections))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private TouchDirections touchDirections;

    [SerializeField] private float walkSpeed = 5f; 
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float jumpInitialSpeed = 10f;
    private bool facingRight = true;
    private bool isMoving;
    private bool isRunning;
    Vector2 moveInput;

    public bool IsMoving {
        get {
            return isMoving;
        }
        private set {
            isMoving = value;
            animator.SetBool(AnimationNames.isMoving, value);
        }
    }

    public bool IsRunning {
        get {
            return isRunning;
        }
        private set {
            isRunning = value;
            animator.SetBool(AnimationNames.isRunning, value);
        }
    }

    public bool FacingRight {
        get {
            return facingRight;
        }
        private set {
            if (facingRight != value) {
                transform.localScale *= new Vector2(-1, 1);
            }
            facingRight = value;
        }
    }

    private float CurrentSpeed {
        get {
            if (!IsMoving || touchDirections.IsOnWall) return 0;
            if (IsMoving && IsRunning) return runSpeed;
            return walkSpeed;
        }
    }

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchDirections = GetComponent<TouchDirections>();
    }

    void FixedUpdate() {
        rigidBody.velocity = new Vector2(moveInput.x * CurrentSpeed, rigidBody.velocity.y);
        animator.SetFloat(AnimationNames.yVelocity, rigidBody.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        changeDirection(moveInput);
    }

    public void OnRun(InputAction.CallbackContext context) {
        if (context.started) {
            IsRunning = true;
        } else if (context.canceled) {
            IsRunning = false;            
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.started && touchDirections.IsOnGround) {
            animator.SetTrigger(AnimationNames.jump);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpInitialSpeed);
        }
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.started) {
            animator.SetTrigger(AnimationNames.attack);
        }
    }

    private void changeDirection(Vector2 moveInput) {
        if (moveInput.x > 0 && !facingRight) {
            FacingRight = true;
        } else if (moveInput.x < 0 && facingRight) {
            FacingRight = false;
        }
    }
}
