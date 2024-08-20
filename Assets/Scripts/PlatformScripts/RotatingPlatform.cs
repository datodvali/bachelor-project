using UnityEngine;

public class RotatingPlatform : CollisionBasedMovingPlatform
{

    [SerializeField] private float _rotationSpeed = 30f;
    [SerializeField] private int _rotationDirection = 1;

    protected override void Move() {
        Vector2 direction = new(_directionX, 0);
        _rb.velocity = direction * _movementSpeed;
        Rotate();
    }

    private void Rotate() {
        transform.Rotate(0,0, _rotationSpeed * _rotationDirection * Time.deltaTime);
    }
}
