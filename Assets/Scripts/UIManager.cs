using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _healTextPrefab;
    [SerializeField] GameObject _damageTextPrefab;
    [SerializeField] Canvas _gameCanvas;

    void Awake() {
        _gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable() {
        CharacterEvents.characterHealed += CharacterHealedHandler;
        CharacterEvents.characterDamaged += CharacterDamagedHandler;
    }

    private void OnDisable() {
        CharacterEvents.characterHealed -= CharacterHealedHandler;
        CharacterEvents.characterDamaged -= CharacterDamagedHandler;
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

    public void OnEscape(InputAction.CallbackContext context) {
        if (context.started) {
            #if (DEVELOPMENT_BUILD || UNITY_EDITOR)
                Debug.Log("Escape button hit");
            #endif
            
            #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                Application.Quit();
            #endif
        }
    }
}
