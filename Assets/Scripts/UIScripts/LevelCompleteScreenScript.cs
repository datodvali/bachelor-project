using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScreenScript : MonoBehaviour
{
    public void GoToTheNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameEvents.levelStarted.Invoke();
    }
}
