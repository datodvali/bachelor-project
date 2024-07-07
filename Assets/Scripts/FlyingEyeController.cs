using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] private DetectionZone _biteDetectionZone;
    private Damageable _damageable;

    private bool _hasTarget = false;

    public Boolean HasTarget {
        get {
            return _hasTarget;
        }
        private set {
            _hasTarget = value;
            _animator.SetBool(AnimationNames.hasTarget, value);
        }
    }

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_damageable.IsAlive) {
            return;
        } 
        HasTarget = _biteDetectionZone.detectedColliders.Count > 0;
    }

    public void OnDamageTaken() {

    }
}
