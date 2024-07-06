using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickUpScript : MonoBehaviour
{
    [SerializeField] private int health = 10;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var damageable)) {
            damageable.Heal(health);
            Destroy(gameObject);
        }
    }
}
