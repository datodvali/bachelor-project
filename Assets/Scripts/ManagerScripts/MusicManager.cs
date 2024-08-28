using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _introMusic, _loopedMusic;
    [SerializeField] private AudioSource _gameOverMusic;
    [SerializeField] private AudioSource _gameCompleteMusic;

    void Start() {
        OnLevelStarted();
    }

    void OnEnable() {
        GameEvents.levelStarted += OnLevelStarted;
        GameEvents.gameOver += OnGameOver;
        GameEvents.gameComplete += OnGameCompleted;
    }

    void OnDisable() {
        GameEvents.levelStarted -= OnLevelStarted;
        GameEvents.gameOver -= OnGameOver;
        GameEvents.gameComplete -= OnGameCompleted;
    }

    private void OnLevelStarted() {
        Debug.Log("music should start");
        _introMusic.Play();
        _loopedMusic.PlayScheduled(AudioSettings.dspTime + _introMusic.clip.length);
    }

    private void OnGameCompleted() {
        _introMusic.Stop();
        _loopedMusic.Stop();
        _gameCompleteMusic.PlayScheduled(AudioSettings.dspTime + 1f);
    }

    private void OnGameOver() {
        _introMusic.Stop();
        _loopedMusic.Stop();
        _gameOverMusic.PlayScheduled(AudioSettings.dspTime + 1f);
    }
}
