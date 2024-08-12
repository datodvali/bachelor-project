using System;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] private DetectionZone _biteDetectionZone;
    private Damageable _damageable;
    [SerializeField] private Collider2D _deathCollider;
    [SerializeField] private List<Transform> _wayPoints;
    private Transform _nextWayPoint;
    private int _wayPointIndex;
    [SerializeField] private float _flightSpeed = 2f;
    private bool _hasTarget = false;
    private float _minDistance = 0.3f;

    public Boolean HasTarget {
        get {
            return _hasTarget;
        }
        private set {
            _hasTarget = value;
            _animator.SetBool(AnimationNames.hasTarget, value);
        }
    }

    public bool LockVelocity {
        get {
            return _animator.GetBool(AnimationNames.lockVelocity);
        }
        set {
            _animator.SetBool(AnimationNames.lockVelocity, value);
        }
    }


    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    void Start()
    {   
        _wayPointIndex = 0;
        _nextWayPoint = _wayPoints[_wayPointIndex];
    }

    void Update()
    {
        if (!_damageable.IsAlive) {
            return;
        } 
        HasTarget = _biteDetectionZone.detectedColliders.Count > 0;
    }

    void FixedUpdate() {
        if (_damageable.IsAlive && !LockVelocity) {
            Fly();
        } else {
            _rb.velocity = Vector2.zero;
        }
    }

    private void Fly()
    {
        Vector2 direction = (_nextWayPoint.position - transform.position).normalized;
        _rb.velocity = direction * _flightSpeed;
        ChangeDirection();
        float distance = Vector2.Distance(_nextWayPoint.position, transform.position);
        if (distance < _minDistance) {
            _wayPointIndex = (_wayPointIndex + 1) % _wayPoints.Count;
            _nextWayPoint = _wayPoints[_wayPointIndex];
        }
    }

    private void ChangeDirection() {
        if (transform.localScale.x * _rb.velocity.x < 0) {
            Vector3 currScale = transform.localScale;  
            transform.localScale = new Vector3(currScale.x * -1, currScale.y, currScale.z);
        } 
    }

    public void OnDeath() {
        Debug.Log("on death called");
        _deathCollider.enabled = true;
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _rb.gravityScale = 2;
    }
}
