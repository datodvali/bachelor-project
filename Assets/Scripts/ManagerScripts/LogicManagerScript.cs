using UnityEngine;

public class LogicManagerScript : MonoBehaviour
{
    public static LogicManagerScript Instance;
    private int _numCoins = 0;
    
    public int NumCoins {
        get {
            return _numCoins;
        }
        private set {
            _numCoins = value;
        }
    }

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

    private void OnGetCoins(int numCoins) {
        NumCoins += numCoins;
    }

    private void GameStartedHandler() {
        NumCoins = 0;
        Time.timeScale = 1;
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
        GameEvents.gameOver += GamePausedHandler; // to stop the passage of time after the game is over
        GameEvents.gameStarted.Invoke();
    }

    void OnDisable() {
        CharacterEvents.coinsClaimed -= OnGetCoins;
        GameEvents.gameStarted -= GameStartedHandler;
        GameEvents.gamePaused -= GamePausedHandler;
        GameEvents.gameResumed -= GameResumedHandler;
        GameEvents.gameOver -= GamePausedHandler;
    }
}
