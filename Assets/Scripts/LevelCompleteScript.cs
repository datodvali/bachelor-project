using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour {

    public static readonly string BestTimeKey = "BestTime{0}";
    public static readonly string MostCoinsKey = "MostCoins{0}";
    public static readonly string LevelUnlockedKey = "Unlocked{0}";
    private readonly int _numberOfLevels = 3;

    private void HandleLevelCompleted() {
        UpdateBestTimeIfNeeded();
        UpdateMostCoinsIfNeeded();
        UnlockNewLevelIfNeeded();
        PlayerPrefs.Save();
    }

    private void UpdateBestTimeIfNeeded() {
        string levelBestTimeKey = string.Format(BestTimeKey, SceneManager.GetActiveScene().buildIndex);
        float prevBestTime = PlayerPrefs.GetFloat(levelBestTimeKey);
        float currTime = LevelTimer.Instance.GetElapsedTime();
        if (prevBestTime == 0 || prevBestTime > currTime) {
            PlayerPrefs.SetFloat(levelBestTimeKey, currTime);
        }
    }

    private void UpdateMostCoinsIfNeeded() {
        string levelMostCoinsKey = string.Format(MostCoinsKey, SceneManager.GetActiveScene().buildIndex);
        int prevMostCoins = PlayerPrefs.GetInt(levelMostCoinsKey);
        int currCoins = LogicManagerScript.Instance.NumCoins;
        if (prevMostCoins < currCoins) {
            PlayerPrefs.SetInt(levelMostCoinsKey, currCoins);
        }
    }

    private void UnlockNewLevelIfNeeded() {
        int currLevel = SceneManager.GetActiveScene().buildIndex;
        if (currLevel == _numberOfLevels) return;

        string levelUnlockedKey = string.Format(LevelUnlockedKey, currLevel + 1);
        if (PlayerPrefs.GetInt(levelUnlockedKey) != 0) return;
        PlayerPrefs.SetInt(levelUnlockedKey, 1);
    }

    void OnEnable() {
        GameEvents.levelComplete += HandleLevelCompleted;
        GameEvents.gameComplete += HandleLevelCompleted;
    }

    void OnDisable() {
        GameEvents.levelComplete -= HandleLevelCompleted;
        GameEvents.gameComplete -= HandleLevelCompleted;
    }
}
