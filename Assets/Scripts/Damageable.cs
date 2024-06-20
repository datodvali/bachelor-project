using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private bool _isAlive = true;
    private int _maxHealth;
    private int _health;
    
    public bool IsAlive {
        get {
            return _isAlive;
        }
        private set {
            _isAlive = value;
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
            if (_health < 0) {
                _health = 0;
                IsAlive = false;
            }
        }
    }

    public void OnHit(int damage) {
        if (IsAlive) {
            Health -= damage;
        }
    }
}
