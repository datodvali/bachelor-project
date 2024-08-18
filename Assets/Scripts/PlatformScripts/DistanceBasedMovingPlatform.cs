using System;
using Unity.VisualScripting;
using UnityEngine;

public class DistanceBasedMovingPlatform : PlatformMovementScript {
    [SerializeField] private float _distanceToCover = 0f;
    private float _startingX; 
    
    protected override void Awake()
    {
        base.Awake();
        _startingX = transform.localPosition.x;
    }

    protected override void Move() {
        Vector2 direction = new(transform.localScale.x, 0);
        _rb.velocity = direction * _movementSpeed;
        if (Math.Abs(_startingX - transform.localPosition.x) > _distanceToCover) {
            _startingX = transform.localPosition.x;
            Vector2 prevScale = transform.localScale;
            transform.localScale = new(prevScale.x * -1, prevScale.y); 
        }
    }
}