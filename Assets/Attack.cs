using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Collider2D _collider;
    [SerializeField] private Vector2 _knockBack = Vector2.zero;
    [SerializeField] private int _damage;

    void Awake() {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null) {
            Vector2 kb = transform.parent.localScale.x > 0 ? _knockBack : new Vector2(_knockBack.x * -1, _knockBack.y);
            damageable.OnHit(_damage, kb);
        }
    }
}
