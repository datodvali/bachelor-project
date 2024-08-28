using UnityEngine;

public class BasePickUpScript : MonoBehaviour {
    private AudioSource _sfx;

    protected virtual void Awake() {
        _sfx = GetComponent<AudioSource>();
    }

    protected virtual void OnClaimed() {
        if (_sfx != null) {
            AudioSource.PlayClipAtPoint(_sfx.clip, gameObject.transform.position, _sfx.volume);
        }
        Destroy(gameObject);
    }
}