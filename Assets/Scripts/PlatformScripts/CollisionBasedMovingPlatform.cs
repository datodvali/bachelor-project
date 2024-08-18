using UnityEngine;

public class CollisionBasedMovingPlatform : PlatformMovementScript {
    private readonly string _layerToCheck = "Ground";
    [SerializeField] protected float _directionX = 1;

    protected override void Move() {
        Vector2 direction = new(_directionX, 0);
        _rb.velocity = direction * _movementSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (LayerMask.NameToLayer(_layerToCheck) == collider.gameObject.layer) {
            _directionX *= -1;
        }
    }
}