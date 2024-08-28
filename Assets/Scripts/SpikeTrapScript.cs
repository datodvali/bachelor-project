using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    [SerializeField] private int _damage = 100;
    private Vector2 _knockBack = Vector2.up * 10;
    private AudioSource _sfx;
    private Damageable _damageableInsideZone;

    void Awake() {
        _sfx = GetComponent<AudioSource>();
    }

    void Update() {
        if (_damageableInsideZone != null) Damage();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var damageable)) {
            _damageableInsideZone = damageable;
            Damage();
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.TryGetComponent<Damageable>(out var damageable)) {
            Damage();
            _damageableInsideZone = null;
        }
    }

    private void Damage() {
        if (_damageableInsideZone != null) _damageableInsideZone.OnHit(_damage, _knockBack);
        if (_sfx != null) AudioSource.PlayClipAtPoint(_sfx.clip, gameObject.transform.position, _sfx.volume);
    }
}
