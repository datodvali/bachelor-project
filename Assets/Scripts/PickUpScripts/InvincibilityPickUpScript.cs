using UnityEngine;

public class InvincibilityPickUpScript : MonoBehaviour
{
    private AudioSource _sfx;
    [SerializeField] private float _invincibilityDuration = 10f;

    void Awake() {
        _sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var playerDamageable)) {
            playerDamageable.OnInvincibilityGained(_invincibilityDuration);
            if (_sfx != null) {
                AudioSource.PlayClipAtPoint(_sfx.clip, gameObject.transform.position, _sfx.volume);
            }
            Destroy(gameObject);
        }
    }
}
