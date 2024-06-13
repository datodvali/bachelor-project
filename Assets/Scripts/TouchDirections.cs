using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchDirections : MonoBehaviour
{
    
    private float groundDistance = 0.05f;
    private float wallDistance = 0.2f;
    private float ceilingDistance = 0.05f;

    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isOnWall;
    [SerializeField] private bool isOnCeiling;

    ContactFilter2D contactFilter;
    CapsuleCollider2D collider;
    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left; 

    public bool IsOnGround {
        get {
            return isOnGround;
        }
        private set {
            isOnGround = value;
            animator.SetBool(AnimationNames.isOnGround, value);
        }
    }

    public bool IsOnWall {
        get {
            return isOnWall;
        }
        private set {
            isOnWall = value;
            animator.SetBool(AnimationNames.isOnWall, value);
        }
    }

    public bool IsOnCeiling {
        get {
            return isOnCeiling;
        }
        private set {
            isOnCeiling = value;
            animator.SetBool(AnimationNames.isOnCeiling, value);
        }
    }

    void Awake() {
        collider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        IsOnGround = collider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = collider.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = collider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
