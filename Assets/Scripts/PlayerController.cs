using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;

    public float walkSpeed = 5f; 
    public float runSpeed = 8f;

    Vector2 moveInput;

    private bool _isMoving;

    public bool IsMoving {
        get {
            return _isMoving;
        }
        private set {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

        private bool _isRunning;

    public bool IsRunning {
        get {
            return _isRunning;
        }
        private set {
            _isRunning = value;
            animator.SetBool("isRunning", value);
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
        rigidBody.velocity = new Vector2(moveInput.x * (IsRunning ? runSpeed : walkSpeed), rigidBody.velocity.y);
    }

    // void FixedUpdate() {
    //     rigidBody.velocity = new Vector2(moveInput.x * walkSpeed, rigidBody.velocity.y);
    // }

    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    public void OnRun(InputAction.CallbackContext context) {
        if (context.started) {
            IsRunning = true;
        } else if (context.canceled) {
            IsRunning = false;            
        }
    }
}
