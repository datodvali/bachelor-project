using System.Collections.Generic;
using UnityEngine;

public class CollisionBasedMovingPlatform : PlatformMovementScript {
    private readonly List<string> _layersToCheck = new();

    [SerializeField] protected int _directionX = 1;

    protected override void Awake() {
        base.Awake();
        _layersToCheck.Add("Ground");
        _layersToCheck.Add("Platform");
    }

    protected override void Move() {
        Vector2 direction = new(_directionX, 0);
        _rb.velocity = direction * _movementSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        foreach (string layerName in _layersToCheck) {
            if (LayerMask.NameToLayer(layerName) == collider.gameObject.layer) {
                _directionX *= -1;
                return;
            }            
        }
    }
}