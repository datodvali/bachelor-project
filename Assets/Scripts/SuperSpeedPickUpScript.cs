using UnityEngine;

public class SuperSpeedPickUpScript : MonoBehaviour
{
    private AudioSource _sfx;
    [SerializeField] private float _superSpeedDuration = 10f;

    void Awake() {
        _sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player)) {
            player.OnSuperSpeedGained(_superSpeedDuration);
            if (_sfx != null) {
                AudioSource.PlayClipAtPoint(_sfx.clip, gameObject.transform.position, _sfx.volume);
            }
            Destroy(gameObject);
        }
    }
}
