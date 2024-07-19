using UnityEngine;

public class SecondLifePickUpScript : MonoBehaviour
{
    private AudioSource _healSfx;

    void Awake() {
        _healSfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player)) {
            Debug.Log("got player controller");
            player.OnSecondLifeGained();
            if (_healSfx != null) {
                AudioSource.PlayClipAtPoint(_healSfx.clip, gameObject.transform.position, _healSfx.volume);
            }
            Destroy(gameObject);
        }
    }
}
