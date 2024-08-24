using UnityEngine;

public class BombTrapScript : MonoBehaviour
{
    private Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D collider) {
        GameObject collidingObject = collider.gameObject;
        if (collidingObject.CompareTag("Player") || collidingObject.layer == LayerMask.NameToLayer("Projectile")) {
            _animator.SetTrigger(AnimationNames.explosion);
        }
    }
    
    public void DestroyBomb() {
        Destroy(gameObject);
    }
}
