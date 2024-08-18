using System.Collections.Generic;
using UnityEngine;

public class WayPointBasedMovingPlatform : PlatformMovementScript
{
    [SerializeField] private List<Transform> _wayPoints;
    private Transform _nextWayPoint;
    private int _wayPointIndex;
    private readonly float _minDistance = 0.3f;


    protected override void Awake()
    {
        base.Awake();
        _wayPointIndex = 0;
        _nextWayPoint = _wayPoints[_wayPointIndex];
    }

    protected override void Move() {
        Vector2 direction = (_nextWayPoint.position - transform.position).normalized;
        _rb.velocity = direction * _movementSpeed;
        float distance = Vector2.Distance(_nextWayPoint.position, transform.position);
        if (distance < _minDistance) {
            _wayPointIndex = (_wayPointIndex + 1) % _wayPoints.Count;
            _nextWayPoint = _wayPoints[_wayPointIndex];
        }
    }
}
