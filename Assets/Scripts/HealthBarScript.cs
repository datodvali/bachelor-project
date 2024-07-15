using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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
    }

    void OnDisable() {
        _damageable.healthUpdateEvent.RemoveListener(HandleHealthUpdate);
    }

    private void HandleHealthUpdate() {
        _healthBar.value = ((float)_damageable.health / _damageable.maxHealth);
    }
}
