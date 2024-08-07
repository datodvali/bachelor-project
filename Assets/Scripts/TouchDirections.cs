using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TouchDirections : MonoBehaviour
{
    [SerializeField] private float _groundDistance = 0.05f;
    [SerializeField] private float _wallDistance = 0.2f;
    [SerializeField] private float _ceilingDistance = 0.05f;
    [SerializeField] private float _platformDistance = 0.05f;

    [SerializeField] private bool _isOnGround;
    [SerializeField] private bool _isOnWall;
    [SerializeField] private bool _isOnCeiling;
    [SerializeField] private bool _isOnPlatform;

    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private ContactFilter2D _platformContactFilter;
    
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    private Animator _animator;

    private RaycastHit2D[] _groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] _wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];
    private RaycastHit2D[] _platformHits = new RaycastHit2D[5];

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

    public bool IsOnPlatform {
        get {
            return _isOnPlatform;
        }
        private set {
            _isOnPlatform = value;
            if (_isOnPlatform) {
                IsOnGround = true;
            }
        }
    }

    void Awake() {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        IsOnGround = _capsuleCollider.Cast(Vector2.down, _contactFilter, _groundHits, _groundDistance) > 0;
        IsOnWall = _capsuleCollider.Cast(WallCheckDirection, _contactFilter, _wallHits, _wallDistance) > 0;
        IsOnCeiling = _capsuleCollider.Cast(Vector2.up, _contactFilter, _ceilingHits, _ceilingDistance) > 0;
        IsOnPlatform = _capsuleCollider.Cast(Vector2.down, _platformContactFilter, _platformHits, _platformDistance) > 0;
    }
}
