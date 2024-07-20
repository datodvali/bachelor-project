using UnityEngine;

public class SuperSpeedPickUpScript : MonoBehaviour
{
    private AudioSource sfx;

    void Awake() {
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player)) {
            player.OnSuperSpeedGained(10f);
            if (sfx != null) {
                AudioSource.PlayClipAtPoint(sfx.clip, gameObject.transform.position, sfx.volume);
            }
            Destroy(gameObject);
        }
    }
}
