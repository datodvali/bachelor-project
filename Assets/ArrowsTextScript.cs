using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowsTextScript : MonoBehaviour
{
    private readonly int _initialNumArrows = 10;
    private TextMeshProUGUI _textMeshPro;

    void Awake() {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        UpdateArrowsText(_initialNumArrows);
    }

    public void UpdateArrowsText(int numArrows) {
        _textMeshPro.text = $"X {numArrows}";
    }

    void OnEnable() {
        CharacterEvents.numArrowsChanged += UpdateArrowsText;
    }

    void OnDisable() {
        CharacterEvents.numArrowsChanged -= UpdateArrowsText;
    }
}
