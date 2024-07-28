using UnityEngine;

public class LogicManagerScript : MonoBehaviour
{
    private int _currLevel = 1;
    private int _numCoins = 0;
    
    public int CurrLevel {
        get {
            return _currLevel;
        }
        set {
            _currLevel = value; 
        }
    }

    public int NumCoins {
        get {
            return _numCoins;
        }
        private set {
            _numCoins = value;
        }
    }

    private void OnGetCoins(int numCoins) {
        NumCoins += numCoins;
    }

    private void GamePausedHandler() {
        Time.timeScale = 0;
    }

    private void GameResumedHandler() {
        Time.timeScale = 1;
    }

    void OnEnable() {
        CharacterEvents.coinsClaimed += OnGetCoins;
        GameEvents.gamePaused += GamePausedHandler;
        GameEvents.gameResumed += GameResumedHandler;
    }

    void OnDisable() {
        CharacterEvents.coinsClaimed -= OnGetCoins;
        GameEvents.gamePaused -= GamePausedHandler;
        GameEvents.gameResumed -= GameResumedHandler;
    }
}
