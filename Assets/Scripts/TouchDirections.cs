using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchDirections : MonoBehaviour
{
    
    public float groundDistance = 0.05f;
    private bool isOnTheGround;

    ContactFilter2D contactFilter;
    CapsuleCollider2D collider;
    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[3];
    
    public bool IsOnTheGround {
        get {
            return isOnTheGround;
        }
        private set {
            isOnTheGround = value;
            animator.SetBool(AnimationNames.isOnTheGround, value);
        }
    }

    void Awake() {
        collider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        IsOnTheGround = collider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;         
        Debug.Log(IsOnTheGround);
    }
}
