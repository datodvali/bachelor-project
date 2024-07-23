using UnityEngine;

public class InvincibilityPickUpScript : MonoBehaviour
{
    private AudioSource sfx;

    void Awake() {
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var playerDamageable)) {
            playerDamageable.OnInvincibilityGained(10f);
            if (sfx != null) {
                AudioSource.PlayClipAtPoint(sfx.clip, gameObject.transform.position, sfx.volume);
            }
            Destroy(gameObject);
        }
    }
}
