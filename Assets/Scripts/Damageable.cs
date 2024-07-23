using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private PlayerVisualController _playerVisualController;
    [SerializeField] private UnityEvent<int, Vector2> _damageEvent;
    public UnityEvent deathEvent;
    public UnityEvent healthUpdateEvent;
    private Animator _animator;
    
    private bool _isAlive = true;
    public int maxHealth = 100;
    public int health = 100;
    

    [SerializeField] private bool _isInvincible;
    private float _invincibilityTime;
    [SerializeField] private float _invincibilityTimeOnHit;

    public bool IsAlive {
        get {
            return _isAlive;
        }
        set {
            _animator.SetBool(AnimationNames.isAlive, value);
            if (_isAlive && !value) {
                _isAlive = value;
                deathEvent.Invoke();
            } else {
                _isAlive = value;
            }
        }
    }

    public int MaxHealth {
        get {
            return maxHealth;
        }
        private set{
            maxHealth = value;
        }
    }

    public int Health {
        get {
            return health;
        }
        set {
            health = value;
            if (health <= 0) {
                health = 0;
            }
            if (health > 100) {
                health = 100;
            }
            healthUpdateEvent.Invoke();
            if (health == 0) IsAlive = false;
        }
    }

    public void Awake() {
        _animator = GetComponent<Animator>();
        _playerVisualController = GetComponent<PlayerVisualController>();
    }

    public void Update() {
        if (!_isInvincible) return;
        _invincibilityTime -= Time.deltaTime;
        if (_invincibilityTime <= 0) {
            _isInvincible = false;
            _invincibilityTime = 0;
            _playerVisualController.ApplyNormalEffects();
        } else {
            ApplyPulseEffectIfNecessary();
        }
    }

    public void OnHit(int damageAmount, Vector2 knockBack) {
        if (IsAlive && !_isInvincible) {
            _animator.SetTrigger(AnimationNames.hit);
            damageAmount = Math.Min(health, damageAmount);
            Health -= damageAmount;
            if (_invincibilityTimeOnHit > 0) ActivateInvincibilityOnHit();
            _damageEvent.Invoke(damageAmount, knockBack);
            CharacterEvents.characterDamaged(gameObject, damageAmount);
        }
    }

    public void Heal(int healAmount)
    {
        if (IsAlive) {
            healAmount = Math.Min(maxHealth - health, healAmount);
            Health += healAmount;
            CharacterEvents.characterHealed(gameObject, healAmount);
        }
    }

    public void OnInvincibilityGained(float invincibilityTime) {
        _isInvincible = true;
        _invincibilityTime = invincibilityTime;
        _playerVisualController.ApplyInvincibilityEffects();
    }

    private void ActivateInvincibilityOnHit() {
        if (!_isInvincible) {
            _isInvincible = true;
            _invincibilityTime = _invincibilityTimeOnHit;
            _playerVisualController.ApplyInvincibilityEffects();
        }
    }

    private void ApplyPulseEffectIfNecessary() {
        if (_invincibilityTime <= 2f) {
            _playerVisualController.ApplyPulsingEffects();
        }
    }
}
