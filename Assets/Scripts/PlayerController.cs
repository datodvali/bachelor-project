using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;

    private bool isMoving;
    public float walkSpeed = 5f; 
    public float runSpeed = 12f;
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
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
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

    private void changeDirection(Vector2 moveInput) {
        if (moveInput.x > 0 && !facingRight) {
            FacingRight = true;
        } else if (moveInput.x < 0 && facingRight) {
            FacingRight = false;
        }
    }
}
