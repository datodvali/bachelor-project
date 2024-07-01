using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _healTextPrefab;
    [SerializeField] GameObject _damageTextPrefab;
    [SerializeField] Canvas _gameCanvas;

    void Awake() {
        _gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable() {
        CharacterEvents.characterHealed += CharacterHealed;
        CharacterEvents.characterDamaged += CharacterDamaged;
    }

    private void OnDisable() {
        CharacterEvents.characterHealed -= CharacterHealed;
        CharacterEvents.characterDamaged -= CharacterDamaged;
    }

    private void CharacterHealed(GameObject character, float heal) {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        Instantiate(_healTextPrefab, spawnPosition, Quaternion.identity, _gameCanvas.transform)
            .GetComponent<TMP_Text>()
            .SetText(heal.ToString());
    }

    private void CharacterDamaged(GameObject character, float damage) {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        Instantiate(_damageTextPrefab, spawnPosition, Quaternion.identity, _gameCanvas.transform)
            .GetComponent<TMP_Text>()
            .SetText(damage.ToString());
    }
}
