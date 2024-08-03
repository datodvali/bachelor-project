using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour {

    private readonly string SavedTimeKey = "BestTime{0}";
    private readonly string SavedCoinsKey = "MostCoins{}";

    private string GetSavedTimeKey() {
        return string.Format(SavedTimeKey, SceneManager.GetActiveScene().name);
    }

    private string GetSavedCoinsKey() {
        return string.Format(SavedCoinsKey, SceneManager.GetActiveScene().name);
    }

    private void HandleLevelCompleted() {
        UpdateBestTimeIfNeeded();
        UpdateMostCoinsIfNeeded();
    }

    private void UpdateBestTimeIfNeeded() {
        string levelBestTimeKey = GetSavedTimeKey();
        float prevBestTime = PlayerPrefs.GetFloat(levelBestTimeKey);
        float currTime = LevelTimer.Instance.GetElapsedTime();
        if (prevBestTime == 0 || prevBestTime > currTime) {
            PlayerPrefs.SetFloat(levelBestTimeKey, currTime);
        }
    }

    private void UpdateMostCoinsIfNeeded() {
        string levelMostCoinsKey = GetSavedCoinsKey();
        float prevMostCoins = PlayerPrefs.GetFloat(levelMostCoinsKey);
        float currCoins = LogicManagerScript.Instance.NumCoins;
        if (prevMostCoins < currCoins) {
            PlayerPrefs.SetFloat(levelMostCoinsKey, currCoins);
        }
    }

    void OnEnable() {
        float prevBestTime = PlayerPrefs.GetFloat(GetSavedTimeKey());
        Debug.Log(String.Format("best time {0}", prevBestTime));
        GameEvents.levelComplete += HandleLevelCompleted;
    }

    void OnDisable() {
        GameEvents.levelComplete -= HandleLevelCompleted;
    }
}
