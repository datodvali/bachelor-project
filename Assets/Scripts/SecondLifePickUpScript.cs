using UnityEngine;

public class SecondLifePickUpScript : MonoBehaviour
{
    private AudioSource secondLifeSfx;

    void Awake() {
        secondLifeSfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player) && !player.hasSecondLife) {
            player.OnSecondLifeGained();
            if (secondLifeSfx != null) {
                AudioSource.PlayClipAtPoint(secondLifeSfx.clip, gameObject.transform.position, secondLifeSfx.volume);
            }
            Destroy(gameObject);
        }
    }
}
