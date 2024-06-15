using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TouchDirections : MonoBehaviour
{
    
    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private float wallDistance = 0.2f;
    [SerializeField] private float ceilingDistance = 0.05f;

    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isOnWall;
    [SerializeField] private bool isOnCeiling;

    [SerializeField] private ContactFilter2D contactFilter;
    
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    private Animator animator;

    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

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
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        IsOnGround = capsuleCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = capsuleCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = capsuleCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
