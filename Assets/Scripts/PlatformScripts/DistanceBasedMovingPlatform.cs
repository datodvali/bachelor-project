using System;
using UnityEngine;

public class DistanceBasedMovingPlatform : PlatformMovementScript {
    [SerializeField] private float _distanceToCover = 0f;
    [SerializeField] private int _directionX = 1;
    private float _startingX; 
    
    protected override void Awake()
    {
        base.Awake();
        _startingX = transform.localPosition.x;
    }

    protected override void Move() {
        Vector2 direction = new(_directionX, 0);
        _rb.velocity = direction * _movementSpeed;
        if (Math.Abs(_startingX - transform.localPosition.x) > _distanceToCover) {
            _startingX = transform.localPosition.x;
            _directionX *= -1;
        }
    }
}