using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private UnityEvent<int, Vector2> _damageEvent;
    [SerializeField] private UnityEvent _deathEvent;
    private Animator _animator;
    
    private bool _isAlive = true;
    private int _maxHealth = 100;
    [SerializeField] private int _health = 100;
    

    [SerializeField] private bool _isInvincible;
    private float _timeSinceLastHit = 0f;
    [SerializeField] private float _invincibilityTime;

    public bool IsAlive {
        get {
            return _isAlive;
        }
        private set {
            _isAlive = value;
            _animator.SetBool(AnimationNames.isAlive, value);
            if (!_isAlive) {
                _deathEvent.Invoke();
            }
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
            if (_health > 100) {
                _health = 100;
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

    public void OnHit(int damageAmount, Vector2 knockBack) {
        if (IsAlive && !_isInvincible) {
            _animator.SetTrigger(AnimationNames.hit);
            damageAmount = Math.Min(_health, damageAmount);
            Health -= damageAmount;
            if (_invincibilityTime > 0)_isInvincible = true;
            _damageEvent.Invoke(damageAmount, knockBack);
            CharacterEvents.characterDamaged(gameObject, damageAmount);
        }
    }

    public void Heal(int healAmount)
    {
        if (IsAlive) {
            healAmount = Math.Min(_maxHealth - _health, healAmount);
            Health += healAmount;
            CharacterEvents.characterHealed(gameObject, healAmount);
        }
    }
}
