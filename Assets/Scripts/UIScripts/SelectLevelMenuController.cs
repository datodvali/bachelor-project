using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelMenuController : MonoBehaviour {

    private readonly int _lastLevelIndex = 3;
    private Canvas _gameCanvas;
    private readonly string _levelButtonTag = "ButtonLevel{0}";
    private readonly string _levelBestTimeTag = "BestTimeLevel{0}";
    private readonly string _levelMostCoinsTag = "MostCoinsLevel{0}";
    private int _lowestLockedLevel = 4;

    void OnEnable() {
        PlayerPrefs.SetInt("Unlocked2", 1);
        PlayerPrefs.SetInt("Unlocked3", 1);
        PlayerPrefs.Save();
        _gameCanvas = FindObjectOfType<Canvas>();
        UpdateLevelLocks();
        UpdateLevelBestScores();
    }

    private void UpdateLevelLocks() {
        for (int i = 2; i <= _lastLevelIndex; i++) {
            if (PlayerPrefs.GetInt(string.Format(LevelCompleteScript.LevelUnlockedKey, i)) == 1) continue;
            LockLevelButton(i);
            if (i < _lowestLockedLevel) _lowestLockedLevel = i;
        }
    }

    private void UpdateLevelBestScores() {
        for (int i = 1; i < _lowestLockedLevel; i++) {
            int numCoins = PlayerPrefs.GetInt(string.Format(LevelCompleteScript.MostCoinsKey, i));
            UpdateTextByTag(string.Format(_levelMostCoinsTag, i), "Coins x " + numCoins.ToString());

            float time = PlayerPrefs.GetFloat(string.Format(LevelCompleteScript.BestTimeKey, i));
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            UpdateTextByTag(string.Format(_levelBestTimeTag, i), formattedTime);
        }
    }

    private void LockLevelButton(int levelIndex) {
        Button button = FindButtonByTag(string.Format(_levelButtonTag, levelIndex));
        button.interactable = false;
    }

    private void UpdateTextByTag(string tag, string newText) {
        TextMeshProUGUI tmp = FindTextByTag(tag);
        tmp.SetText(newText);
    }

    private Button FindButtonByTag(string tag) {
        return _gameCanvas.GetComponentsInChildren<Button>(true).FirstOrDefault(t => t.CompareTag(tag));
    }

    private TextMeshProUGUI FindTextByTag(string tag) {
        return _gameCanvas.GetComponentsInChildren<TextMeshProUGUI>(true).FirstOrDefault(t => t.CompareTag(tag));
    }
}