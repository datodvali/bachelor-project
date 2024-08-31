using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource _introMusic, _loopedMusic;
    [SerializeField] private AudioSource _startScreenMusic;
    [SerializeField] private AudioSource _gameOverMusic;
    [SerializeField] private AudioSource _gameCompleteMusic;
    [SerializeField] private AudioSource _buttonClickSound;
    [SerializeField] private float _totalVolumeIncreaseTime = 2f;
    [SerializeField] private float _timeBeforeStart = 1f;
    private StartScreenMusicManager _startScreenMusicManager = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (SceneManager.GetActiveScene().name == "StartScreen") OnStartScreen();
        else OnLevelStarted();
    }

    void OnEnable() {
        GameEvents.backToStartScreen += OnStartScreen;
        GameEvents.levelStarted += OnLevelStarted;
        GameEvents.gameOver += OnGameOver;
        GameEvents.gameComplete += OnGameCompleted;
    }

    void OnDisable() {
        GameEvents.backToStartScreen -= OnStartScreen;
        GameEvents.levelStarted -= OnLevelStarted;
        GameEvents.gameOver -= OnGameOver;
        GameEvents.gameComplete -= OnGameCompleted;
    }

    private void OnLevelStarted() {
        _startScreenMusicManager.Stop();
        _gameOverMusic.Stop();
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

    private void OnStartScreen() {
        _introMusic.Stop();
        _loopedMusic.Stop();
        _gameOverMusic.Stop();
        _startScreenMusicManager.Play();
    }

    public void OnButtonClick() {
        _buttonClickSound.Play();
    }

    private class StartScreenMusicManager {
        private float _elapsedVolumeIncreaseTime;
        private float _elapsedTimeBeforeStart;
        private float _targetVolume;

        public void Play() {
            Time.timeScale = 1;
            if (MusicManager.Instance._startScreenMusic != null) {
                _targetVolume = MusicManager.Instance._startScreenMusic.volume;
                MusicManager.Instance._startScreenMusic.volume = 0;
                MusicManager.Instance._startScreenMusic.PlayScheduled(AudioSettings.dspTime + MusicManager.Instance._timeBeforeStart);
                MusicManager.Instance.StartCoroutine(IncreaseVolume());
            }
        }

        public void Stop() {
            if (MusicManager.Instance._startScreenMusic != null) {
                MusicManager.Instance._startScreenMusic.Stop();
                _elapsedTimeBeforeStart = 0;
                _elapsedVolumeIncreaseTime = 0;
            }
        }

        IEnumerator IncreaseVolume() {
            while (true) {
                if (!MusicStarted()) yield return null;
                if (_elapsedVolumeIncreaseTime > MusicManager.Instance._totalVolumeIncreaseTime) break;
                _elapsedVolumeIncreaseTime += Time.deltaTime;
                if (_elapsedVolumeIncreaseTime < MusicManager.Instance._totalVolumeIncreaseTime) {
                    MusicManager.Instance._startScreenMusic.volume = _targetVolume * (_elapsedVolumeIncreaseTime / MusicManager.Instance._totalVolumeIncreaseTime);
                } else {
                    MusicManager.Instance._startScreenMusic.volume = _targetVolume;
                }
                yield return null;
            }
        }

        private bool MusicStarted() {
            if (_elapsedTimeBeforeStart > MusicManager.Instance._timeBeforeStart) return true;
            _elapsedTimeBeforeStart += Time.deltaTime;
            return false;
        }
    }
}
