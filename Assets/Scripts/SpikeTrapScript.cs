using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    [SerializeField] private int _damage = 100;
    private Vector2 _knockBack = Vector2.up * 10;
    private AudioSource _sfx;

    void Awake() {
        _sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var damageable)) {
            damageable.OnHit(_damage, _knockBack);
            if (_sfx != null) {
                AudioSource.PlayClipAtPoint(_sfx.clip, gameObject.transform.position, _sfx.volume);
            }
        }
    }
}
