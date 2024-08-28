using UnityEngine;

public class ArrowPickUpScript : BasePickUpScript
{
    [SerializeField] private int _numArrows = 5;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player)) {
            player.OnArrowsClaimed(_numArrows);
            OnClaimed();
        }
    }
}
