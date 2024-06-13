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

    private bool isMoving;
    public float walkSpeed = 5f; 
    public float runSpeed = 12f;
    public float jumpInitialSpeed = 10f;
    private bool facingRight = true;

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

    private bool isRunning;

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

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchDirections = GetComponent<TouchDirections>();
    }

    void FixedUpdate() {
        rigidBody.velocity = new Vector2(moveInput.x * (isRunning ? runSpeed : walkSpeed), rigidBody.velocity.y);
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
        if (context.started && touchDirections.IsOnTheGround) {
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
