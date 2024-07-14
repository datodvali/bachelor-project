using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TouchDirections : MonoBehaviour
{
    
    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private float wallDistance = 0.2f;
    [SerializeField] private float ceilingDistance = 0.05f;

    [SerializeField] private bool _isOnGround;
    [SerializeField] private bool _isOnWall;
    [SerializeField] private bool _isOnCeiling;

    [SerializeField] private ContactFilter2D contactFilter;
    
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    private Animator _animator;

    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private Vector2 WallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left; 

    public bool IsOnGround {
        get {
            return _isOnGround;
        }
        private set {
            _isOnGround = value;
            _animator.SetBool(AnimationNames.isOnGround, value);
        }
    }

    public bool IsOnWall {
        get {
            return _isOnWall;
        }
        private set {
            _isOnWall = value;
            _animator.SetBool(AnimationNames.isOnWall, value);
        }
    }

    public bool IsOnCeiling {
        get {
            return _isOnCeiling;
        }
        private set {
            _isOnCeiling = value;
            _animator.SetBool(AnimationNames.isOnCeiling, value);
        }
    }

    void Awake() {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        IsOnGround = capsuleCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = capsuleCollider.Cast(WallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = capsuleCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
