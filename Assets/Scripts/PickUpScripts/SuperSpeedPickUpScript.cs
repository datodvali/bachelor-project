using UnityEngine;

public class SuperSpeedPickUpScript : BasePickUpScript
{
    [SerializeField] private float _superSpeedDuration = 10f;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player)) {
            player.OnSuperSpeedGained(_superSpeedDuration);
            OnClaimed();
        }
    }
}
