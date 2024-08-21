using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private readonly string _pauseScreenTag = "PauseScreen";
    private readonly string _deathScreenTag = "DeathScreen";
    private readonly string _levelCompleteScreenTag = "LevelCompleteScreen";
    private readonly string _gameCompleteScreenTag = "GameCompleteScreen";
    [SerializeField] GameObject _healTextPrefab;
    [SerializeField] GameObject _damageTextPrefab;
    [SerializeField] GameObject _secondLifeTextPrefab;
    [SerializeField] Canvas _gameCanvas;
    private bool _gamePaused = false;
    private GameObject _pauseScreen;
    private GameObject _deathScreen;
    private GameObject _levelCompleteScreen;
    private GameObject _gameCompleteScreen;

    void Awake() {
        _gameCanvas = FindObjectOfType<Canvas>();
        _pauseScreen = FindByTag(_pauseScreenTag);
        _deathScreen = FindByTag(_deathScreenTag);
        _levelCompleteScreen = FindByTag(_levelCompleteScreenTag);
        _gameCompleteScreen = FindByTag(_gameCompleteScreenTag);
    }

    private GameObject FindByTag(string screenTag) {
        GameObject screen = _gameCanvas.GetComponentsInChildren<RectTransform>(true).FirstOrDefault(t => t.CompareTag(screenTag))?.gameObject;

        if (screen == null) {
            Debug.LogError($"{screenTag} could not be found");
        }
        return screen;
    }

    private void OnEnable() {
        CharacterEvents.characterHealed += CharacterHealedHandler;
        CharacterEvents.characterDamaged += CharacterDamagedHandler;
        CharacterEvents.secondLifeGained += SecondLifeGainedHandler;
        GameEvents.gamePaused += GamePausedHandler;
        GameEvents.gameResumed += GameResumedHandler;
        GameEvents.gameOver += GameOverHandler;
        GameEvents.levelComplete += LevelCompleteHandler;
        GameEvents.gameComplete += GameCompleteHandler;
    }

    private void OnDisable() {
        CharacterEvents.characterHealed -= CharacterHealedHandler;
        CharacterEvents.characterDamaged -= CharacterDamagedHandler;
        CharacterEvents.secondLifeGained -= SecondLifeGainedHandler;
        GameEvents.gamePaused -= GamePausedHandler;
        GameEvents.gameResumed -= GameResumedHandler;
        GameEvents.gameOver -= GameOverHandler;
        GameEvents.levelComplete -= LevelCompleteHandler;
        GameEvents.gameComplete -= GameCompleteHandler;
    }

    private void CharacterHealedHandler(GameObject character, float heal) {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        Instantiate(_healTextPrefab, spawnPosition, Quaternion.identity, _gameCanvas.transform)
            .GetComponent<TMP_Text>()
            .SetText(heal.ToString());
    }

    private void CharacterDamagedHandler(GameObject character, float damage) {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        Transform transform = _gameCanvas.transform;
        Vector3 parentPosition = transform.localPosition;
        transform.localPosition = new Vector3(parentPosition.x, parentPosition.y, parentPosition.z - 10);
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

    private void GameOverHandler() {
        _deathScreen.SetActive(true);
    }

    private void LevelCompleteHandler() {
        _levelCompleteScreen.SetActive(true);
    }

    private void GameCompleteHandler() {
        _gameCompleteScreen.SetActive(true);
    }

    public void OnEscape(InputAction.CallbackContext context) {
        if (context.started && LogicManagerScript.Instance.GameOn) {
            if (_gamePaused) {
                GameEvents.gameResumed.Invoke();
            } else {
                GameEvents.gamePaused.Invoke();
            }
        }
    }
}
