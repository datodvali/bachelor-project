using UnityEngine;

public class VerticalCollisionBasedMovingPlatform : PlatformMovementScript
{
    [SerializeField] private float _directionY = 1;
    private readonly string _layerToCheck = "Ground";

    protected override void Move() {
        Vector2 direction = new(0, _directionY);
        _rb.velocity = direction * _movementSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (LayerMask.NameToLayer(_layerToCheck) == collider.gameObject.layer) {
            _directionY *= -1;
        }
    }
}