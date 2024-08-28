using UnityEngine;

public class SecondLifePickUpScript : BasePickUpScript
{

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player) && !player.hasSecondLife) {
            player.OnSecondLifeGained();
            OnClaimed();
        }
    }
}
