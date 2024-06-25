using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private Animator _animator;
    
    private bool _isAlive = true;
    private int _maxHealth = 100;
    private int _health = 100;
    

    [SerializeField] private bool _isInvincible;
    private float _timeSinceLastHit = 0f;
    private float _invincibilityTime = 0.25f;

    public bool IsAlive {
        get {
            return _isAlive;
        }
        private set {
            _isAlive = value;
            _animator.SetBool(AnimationNames.isAlive, value);
        }
    }

    public int MaxHealth {
        get {
            return _maxHealth;
        }
        private set{
            _maxHealth = value;
        }
    }

    public int Health {
        get {
            return _health;
        }
        private set {
            _health = value;
            if (_health <= 0) {
                _health = 0;
                IsAlive = false;
            }
        }
    }

    public void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void Update() {
        if (_isInvincible) {
            if (_timeSinceLastHit > _invincibilityTime) {
                _isInvincible = false;
                _timeSinceLastHit = 0;
            }
            _timeSinceLastHit += Time.deltaTime;
        }
    }

    public void OnHit(int damage) {
        if (IsAlive && !_isInvincible) {
            Health -= damage;
            _isInvincible = true;
        }
    }
}
