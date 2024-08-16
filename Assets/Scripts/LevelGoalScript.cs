using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelGoalScript : MonoBehaviour
{
    
    private Collider2D _collider;

    void Awake() {
        _collider = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("123123");
        if (collider.TryGetComponent<PlayerController>(out var player)) {
            GameEvents.levelComplete.Invoke();
        }
    }
}
