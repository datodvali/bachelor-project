using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void Play() {
        SceneManager.LoadScene("Level 1");
    }

    public void Exit() {
        Application.Quit();
    }

    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }
}
