using TMPro;
using UnityEngine;

public class CoinsTextScript : MonoBehaviour
{
    private int _numCoins = 0;
    private TextMeshProUGUI _textMeshPro;

    void Awake() {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        UpdateCoinsText(0);
    }

    public void UpdateCoinsText(int coins) {
        _numCoins += coins; 
        _textMeshPro.text = $"X {_numCoins}";
    }

    void OnEnable() {
        CharacterEvents.coinsClaimed += UpdateCoinsText;
    }

    void OnDisable() {
        CharacterEvents.coinsClaimed -= UpdateCoinsText;
    }
}
