using UnityEngine;

public class CoinScript : BasePickUpScript
{
    [SerializeField] private int _numCoins = 1;

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<PlayerController>(out var player)) {
            CharacterEvents.coinsClaimed.Invoke(_numCoins);
            OnClaimed();
        }
    }
}
