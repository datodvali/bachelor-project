using UnityEngine;
using System;
using TMPro;

public class TimeDisplayScript : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    
    void Awake() {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (LevelTimer.Instance != null)
        {
            float elapsedTime = LevelTimer.Instance.GetElapsedTime();
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            _textMeshPro.text = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
    }
}