using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private readonly string _pauseScreenTag = "PauseScreen";
    private readonly string _deathScreenTag = "DeathScreen";
    [SerializeField] GameObject _healTextPrefab;
    [SerializeField] GameObject _damageTextPrefab;
    [SerializeField] GameObject _secondLifeTextPrefab;
    [SerializeField] Canvas _gameCanvas;
    private bool _gamePaused = false;
    private GameObject _pauseScreen;
    private GameObject _deathScreen;

    void Awake() {
        _gameCanvas = FindObjectOfType<Canvas>();
        FindPauseScreen();
        FindDeathScreen();
    }

    private void FindPauseScreen() {
        _pauseScreen = _gameCanvas.GetComponentsInChildren<RectTransform>(true).FirstOrDefault(t => t.CompareTag(_pauseScreenTag))?.gameObject;

        if (_pauseScreen == null) {
            Debug.LogError("PauseScreen not found!");
        }
    }

    private void FindDeathScreen() {
        _deathScreen = _gameCanvas.GetComponentsInChildren<RectTransform>(true).FirstOrDefault(t => t.CompareTag(_deathScreenTag))?.gameObject;

        if (_deathScreen == null) {
            Debug.LogError("DeathScreen not found!");
        }
    }

    private void OnEnable() {
        CharacterEvents.characterHealed += CharacterHealedHandler;
        CharacterEvents.characterDamaged += CharacterDamagedHandler;
        CharacterEvents.secondLifeGained += SecondLifeGainedHandler;
        GameEvents.gamePaused += GamePausedHandler;
        GameEvents.gameResumed += GameResumedHandler;
        GameEvents.gameEnded += GameEndedHandler;
    }

    private void OnDisable() {
        CharacterEvents.characterHealed -= CharacterHealedHandler;
        CharacterEvents.characterDamaged -= CharacterDamagedHandler;
        CharacterEvents.secondLifeGained -= SecondLifeGainedHandler;
        GameEvents.gamePaused -= GamePausedHandler;
        GameEvents.gameResumed -= GameResumedHandler;
        GameEvents.gameEnded -= GameEndedHandler;
    }

    private void CharacterHealedHandler(GameObject character, float heal) {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        Instantiate(_healTextPrefab, spawnPosition, Quaternion.identity, _gameCanvas.transform)
            .GetComponent<TMP_Text>()
            .SetText(heal.ToString());
    }

    private void CharacterDamagedHandler(GameObject character, float damage) {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        Instantiate(_damageTextPrefab, spawnPosition, Quaternion.identity, _gameCanvas.transform)
            .GetComponent<TMP_Text>()
            .SetText(damage.ToString());
    }

    private void SecondLifeGainedHandler(GameObject character) {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        Instantiate(_secondLifeTextPrefab, spawnPosition, Quaternion.identity, _gameCanvas.transform)
            .GetComponent<TMP_Text>()
            .SetText("+1 LIFE");
    }

    private void GamePausedHandler() {
        _gamePaused = true;
        _pauseScreen.SetActive(true);
    }

    private void GameResumedHandler() {
        _gamePaused = false;
        _pauseScreen.SetActive(false);
    }

    private void GameEndedHandler() {
        _deathScreen.SetActive(true);
    }

    public void OnEscape(InputAction.CallbackContext context) {
        if (context.started) {
            #if (DEVELOPMENT_BUILD || UNITY_EDITOR)
                Debug.Log("Escape button hit");
            #endif
            
            if (_gamePaused) {
                GameEvents.gameResumed.Invoke();
            } else {
                GameEvents.gamePaused.Invoke();
            }
        }
    }
}
