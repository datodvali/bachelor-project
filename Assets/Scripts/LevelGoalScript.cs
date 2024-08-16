using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelGoalScript : MonoBehaviour
{

    private readonly int _numberOfLevels = 3;

    private Collider2D _collider;

    void Awake() {
        _collider = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player)) {
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            if (SceneManager.GetActiveScene().buildIndex == _numberOfLevels) {
                GameEvents.gameComplete.Invoke();
            } else {
                GameEvents.levelComplete.Invoke();
            }
        }
    }
}
