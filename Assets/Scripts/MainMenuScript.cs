using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private readonly string _deafultLevel = "Level 1";
    public void Play() {
        SceneManager.LoadScene(_deafultLevel);
        GameEvents.gameStarted.Invoke();
    }

    public void Exit() {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
            Application.Quit();
        #endif
    }

    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
        GameEvents.gameStarted.Invoke();
    }
}
