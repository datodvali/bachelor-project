using UnityEngine;

public class InvincibilityPickUpScript : BasePickUpScript
{
    [SerializeField] private float _invincibilityDuration = 10f;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var playerDamageable)) {
            playerDamageable.OnInvincibilityGained(_invincibilityDuration);
            OnClaimed();
        }
    }
}
