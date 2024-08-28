using UnityEngine;

public class HealthPickUpScript : BasePickUpScript
{
    [SerializeField] private int _health = 10;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var damageable)) {
            damageable.Heal(_health);
            OnClaimed();
        }
    }
}
