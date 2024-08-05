using System;
using UnityEngine;

public class LogicManagerScript : MonoBehaviour
{
    public static LogicManagerScript Instance;
    private int _numCoins = 0;
    private bool _gameOn;

    public int NumCoins {
        get {
            return _numCoins;
        }
        private set {
            _numCoins = value;
        }
    }

    public bool GameOn {
        get {
            return _gameOn;
        }
        private set {
            _gameOn = value;
        }
    }

    private void Awake()
    {
        GameOn = true;
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

    private void OnGetCoins(int numCoins) {
        NumCoins += numCoins;
    }

    private void GameStartedHandler() {
        GameOn = true;
        NumCoins = 0;
        Time.timeScale = 1;
    }

    private void GameOverHandler() {
        GameOn = false;
        GamePausedHandler();
    }

    private void GamePausedHandler() {
        Time.timeScale = 0;
    }

    private void GameResumedHandler() {
        Time.timeScale = 1;
    }

    void OnEnable() {
        CharacterEvents.coinsClaimed += OnGetCoins;
        GameEvents.gameStarted += GameStartedHandler;
        GameEvents.gamePaused += GamePausedHandler;
        GameEvents.gameResumed += GameResumedHandler;
        GameEvents.gameOver += GameOverHandler;
        GameEvents.gameStarted.Invoke();
    }

    void OnDisable() {
        CharacterEvents.coinsClaimed -= OnGetCoins;
        GameEvents.gameStarted -= GameStartedHandler;
        GameEvents.gamePaused -= GamePausedHandler;
        GameEvents.gameResumed -= GameResumedHandler;
        GameEvents.gameOver -= GameOverHandler;
    }
}
