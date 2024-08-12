using UnityEngine;

public class CollisionBasedMovingPlatform : PlatformMovementScript {
    private readonly string _layerToCheck = "Ground";

    protected override void PrepareForMovement()
    {
    }

    protected override void Move() {
        Vector2 direction = new(transform.localScale.x, 0);
        _rb.velocity = direction * _movementSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (LayerMask.NameToLayer(_layerToCheck) == collider.gameObject.layer) {
            Vector2 prevScale = transform.localScale;
            transform.localScale = new(prevScale.x*-1, prevScale.y);
        }
    }
}