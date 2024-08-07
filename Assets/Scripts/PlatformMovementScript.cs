using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private List<Transform> _wayPoints;
    private Transform _nextWayPoint;
    private int _wayPointIndex;
    [SerializeField] private float _movementSpeed = 3f;
    private readonly float _minDistance = 0.3f;

    public Vector2 Velocity {
        get {
            return _rb.velocity;
        }
    }
    
    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {   
        _wayPointIndex = 0;
        _nextWayPoint = _wayPoints[_wayPointIndex];
    }

    void FixedUpdate() {
        Move();
    }

    private void Move() {
        Vector2 direction = (_nextWayPoint.position - transform.position).normalized;
        _rb.velocity = direction * _movementSpeed;
        float distance = Vector2.Distance(_nextWayPoint.position, transform.position);
        if (distance < _minDistance) {
            _wayPointIndex = (_wayPointIndex + 1) % _wayPoints.Count;
            _nextWayPoint = _wayPoints[_wayPointIndex];
        }
    }
}
