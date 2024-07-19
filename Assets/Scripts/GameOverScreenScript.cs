using UnityEngine;

public class GameOverScreenScript : MonoBehaviour
{
    void Awake() {
        GameEvents.gameStarted += OnGameStart;
        GameEvents.gameEnded += OnGameOver;
    }

    // void OnDisable() {
        // GameEvents.gameStart -= OnGameStart;
        // GameEvents.gameOver -= OnGameOver;
    // }

    void OnGameOver() {
        Debug.Log("game has ended");
        gameObject.SetActive(true);
    }

    void OnGameStart() {
        gameObject.SetActive(false);
    }
}
