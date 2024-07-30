using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseScreenScript : MonoBehaviour
{
    private readonly string _startScreenName = "StartScreen"; 
    public void OnResumeHit() {
        GameEvents.gameResumed();
    }

    public void OnMainMenuHit() {
        GameEvents.gameResumed();
        SceneManager.LoadScene(_startScreenName);
    }
}
