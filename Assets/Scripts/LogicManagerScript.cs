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

    public void OnGetCoins(int numCoins) {
        NumCoins += numCoins;
    }

    void OnEnable() {
        CharacterEvents.coinsClaimed += OnGetCoins;
    }

    void OnDisable() {
        CharacterEvents.coinsClaimed -= OnGetCoins;
    }
}
