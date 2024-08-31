using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TouchDirections : MonoBehaviour
{
    [SerializeField] private bool _gravityReversed = false; 
    [SerializeField] private float _groundDistance = 0.05f;
    [SerializeField] private float _wallDistance = 0.2f;
    [SerializeField] private float _ceilingDistance = 0.05f;
    [SerializeField] private float _platformDistance = 0.05f;
    [SerializeField] private bool _isOnGround;
    [SerializeField] private bool _isOnWall;
    [SerializeField] private bool _isOnCeiling;
    [SerializeField] private bool _isOnPlatform;
    [SerializeField] private float _fallDeathDistance;
    private float _fallStartPointY;
    private bool _fatalFall;

    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private ContactFilter2D _platformContactFilter;
    
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    private Animator _animator;

    private RaycastHit2D[] _groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] _wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];
    private RaycastHit2D[] _platformHits = new RaycastHit2D[5];

    private Vector2 WallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left; 

    public bool FatalFall => _fatalFall;

    public bool GravityReversed {
        get {
            return _gravityReversed;
        }
        set {
            _gravityReversed = value;
        }
    }

    public bool IsOnGround {
        get {
            return _isOnGround;
        }
        private set {
            if (value != _isOnGround && !value) _fallStartPointY = gameObject.transform.localPosition.y;
            else if (value) _fallStartPointY = 0;
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
        CheckFatalFall();
        Vector2 groundDirection = GetGroundDirection();
        Vector2 ceilingDirection = GetCeilingDirection();
        IsOnGround = _capsuleCollider.Cast(groundDirection, _contactFilter, _groundHits, _groundDistance) > 0;
        IsOnWall = _capsuleCollider.Cast(WallCheckDirection, _contactFilter, _wallHits, _wallDistance) > 0;
        IsOnCeiling = _capsuleCollider.Cast(ceilingDirection, _contactFilter, _ceilingHits, _ceilingDistance) > 0;
        IsOnPlatform = _capsuleCollider.Cast(groundDirection, _platformContactFilter, _platformHits, _platformDistance) > 0;

    }

    private Vector2 GetGroundDirection() {
        return !GravityReversed ? Vector2.down : Vector2.up; 
    }

    private Vector2 GetCeilingDirection() {
        return !GravityReversed ? Vector2.up : Vector2.down;
    }

    private void CheckFatalFall() {
        if (IsOnGround && !_fatalFall) return;
        if (Math.Abs(_fallStartPointY - gameObject.transform.localPosition.y) > _fallDeathDistance) _fatalFall = true;
    }
}
