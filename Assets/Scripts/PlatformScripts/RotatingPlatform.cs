using UnityEngine;
using UnityEngine.UIElements;

public class RotatingPlatform : CollisionBasedMovingPlatform
{

    [SerializeField] private float _rotationSpeed = 30f;
    public float slideSpeed = 5f;

    protected override void Move() {
        Vector2 direction = new(_directionX, 0);
        _rb.velocity = direction * _movementSpeed;
        Rotate();
    }

    private void Rotate() {
        transform.Rotate(0,0, _rotationSpeed * Time.deltaTime);
    }

    // void OnCollisionStay2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         Rigidbody2D character = collision.gameObject.GetComponent<Rigidbody2D>();

    //         float platformAngle = transform.rotation.eulerAngles.z;
    //         if (platformAngle > 180) platformAngle -= 360;
    //         float angleInRadians = platformAngle * Mathf.Deg2Rad;

    //         Vector2 slideDirection = new(-Mathf.Sin(angleInRadians), -Mathf.Cos(angleInRadians));
    //         character.velocity = new(character.velocity.x + slideDirection.x * slideSpeed, character.velocity.y + slideDirection.y * slideSpeed);
    //     }
    // }
}
