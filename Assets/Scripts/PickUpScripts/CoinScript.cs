using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int _numCoins = 1;
    private AudioSource _healSfx;

    void Awake() {
        _healSfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player)) {
            CharacterEvents.coinsClaimed.Invoke(_numCoins);
            if (_healSfx != null) {
                AudioSource.PlayClipAtPoint(_healSfx.clip, gameObject.transform.position, _healSfx.volume);
            }
            Destroy(gameObject);
        }
    }
}
