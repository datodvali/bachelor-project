using Unity.VisualScripting;
using UnityEngine;

public class BombTrapScript : MonoBehaviour
{
    private Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
    }
    public void OnCollisionEnter2D(Collision2D collision) {
        GameObject collidingObject = collision.collider.gameObject;
        if (collidingObject.CompareTag("Player")) {
            _animator.SetTrigger(AnimationNames.explosion);
        }
    }
    
    public void DestroyBomb() {
        Destroy(gameObject);
    }
}
