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
            if (SceneManager.GetActiveScene().buildIndex == _numberOfLevels) {
                Invoke(nameof(InvokeGameCompleteEvent), 2f);
            } else {
                Invoke(nameof(InvokeLevelCompleteEvent), 2f);
            }
        }
    }

    private void InvokeGameCompleteEvent() {
        GameEvents.gameComplete.Invoke();
    }

    private void InvokeLevelCompleteEvent() {
        GameEvents.levelComplete.Invoke();
    }
}
