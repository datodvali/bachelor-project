using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private UnityEvent<int, Vector2> _damageEvent;
    public UnityEvent deathEvent;
    public UnityEvent healthUpdateEvent;
    private Animator _animator;
    
    private bool _isAlive = true;
    public int maxHealth = 100;
    public int health = 100;
    

    [SerializeField] private bool _isInvincible;
    private float _timeSinceLastHit = 0f;
    [SerializeField] private float _invincibilityTime;

    public bool IsAlive {
        get {
            return _isAlive;
        }
        set {
            _isAlive = value;
            _animator.SetBool(AnimationNames.isAlive, value);
            if (!_isAlive) {
                deathEvent.Invoke();
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
                IsAlive = false;
            }
            if (health > 100) {
                health = 100;
            }
            healthUpdateEvent.Invoke();
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
            damageAmount = Math.Min(health, damageAmount);
            Health -= damageAmount;
            if (_invincibilityTime > 0) _isInvincible = true;
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
}
