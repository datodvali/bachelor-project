using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseScreenScript : MonoBehaviour
{
    private readonly string _startScreenName = "StartScreen"; 
    public void OnResumeHit() {
        GameEvents.gameResumed();
    }

    public void OnExitHit() {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
            Application.Quit();
        #endif
    }

    public void OnMainMenuHit() {
        GameEvents.gameResumed();
        SceneManager.LoadScene(_startScreenName);
    }
}
