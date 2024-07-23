using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Damageable _damageable;
    private Slider _healthBar;

    void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null) {
            Debug.Log("Could not find any GameObjects with the tag Player");
            return;
        }
        _damageable = playerObject.GetComponent<Damageable>();
        _healthBar = GetComponent<Slider>();
    }

    void OnEnable() {
        HandleHealthUpdate();
        _damageable.healthUpdateEvent.AddListener(HandleHealthUpdate);
        CharacterEvents.secondLifeGained += HandleSecondLifeGained;
        
    }

    void OnDisable() {
        _damageable.healthUpdateEvent.RemoveListener(HandleHealthUpdate);
        CharacterEvents.secondLifeGained -= HandleSecondLifeGained;
    }

    private void HandleHealthUpdate() {
        _healthBar.value = ((float) _damageable.health / _damageable.maxHealth);
        if (_healthBar.value <= 0) HandleLifeDepleted();
    }

    private void HandleSecondLifeGained(GameObject gameObject) {
        ColorBlock colors = _healthBar.colors;
        colors.disabledColor = new Color(255,215,0);
        _healthBar.colors = colors;
    }

    private void HandleLifeDepleted() {
        ColorBlock colors = _healthBar.colors;
        colors.disabledColor = new Color32(57,231,67,128);
        _healthBar.colors = colors;
    }
}
