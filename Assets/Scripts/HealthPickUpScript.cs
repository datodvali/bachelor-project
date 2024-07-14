using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickUpScript : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    private AudioSource _healSfx;

    void Awake() {
        _healSfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var damageable)) {
            damageable.Heal(_health);
            if (_healSfx != null) {
                AudioSource.PlayClipAtPoint(_healSfx.clip, gameObject.transform.position, _healSfx.volume);
            }
            Destroy(gameObject);
        }
    }
}
