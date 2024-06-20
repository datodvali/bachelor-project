using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchDirections), typeof(Animator))]
public class KnightLogic : MonoBehaviour
{

    [SerializeField] private float runSpeed = 5f;
    private Rigidbody2D rigidBody;
    private TouchDirections touchDirections;
    [SerializeField] private DetectionZone attackZone;
    private Animator animator;

    public enum Direction {RIGHT, LEFT}
    [SerializeField] private Direction moveDirection;
    private bool hasTarget = false;
    public Direction MoveDirection {
        get {
            return moveDirection;
        }
        private set {
            moveDirection = value;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    public Boolean HasTarget {
        get {
            return hasTarget;
        }
        private set {
            hasTarget = value;
            animator.SetBool(AnimationNames.attack, value);
        }
    }

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        touchDirections = GetComponent<TouchDirections>();
        animator = GetComponent<Animator>();
    }

    void Start() {
        animator.SetBool(AnimationNames.isMoving, true);
    }

    void Update() {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    void FixedUpdate() {
        if (touchDirections.IsOnGround && touchDirections.IsOnWall) {
            ChangeDirection();
        }
        rigidBody.velocity = new Vector2(runSpeed* (MoveDirection == Direction.RIGHT ? 1 : -1),rigidBody.velocity.y);
    }

    private void ChangeDirection() {
        if (MoveDirection == Direction.RIGHT) {
            MoveDirection = Direction.LEFT;
        } else if (MoveDirection == Direction.LEFT) {
            MoveDirection = Direction.RIGHT;
        } else {
            Debug.LogError("Knight move direction is not set to any legal value");
        }
    }
}
