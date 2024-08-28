using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverScreenScript : MonoBehaviour
{
    private readonly string _startScreenName = "StartScreen"; 

    public void OnRestartLevelHit() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameEvents.levelStarted.Invoke();
    }

    public void OnMainMenuHit() {
        SceneManager.LoadScene(_startScreenName);
    }
}
