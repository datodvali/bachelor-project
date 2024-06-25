using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform target;

    private Vector2 _startPosition;
    private float _startZ;
    private float DistanceFromTarget => transform.position.z - target.position.z;
    private float ClippingPlane => cam.transform.position.z + (DistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane);
    private float ParallaxFactor => Mathf.Abs(DistanceFromTarget) / ClippingPlane;

    Vector2 TravelDistance => (Vector2) cam.transform.position - _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;   
        _startZ = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPosition = _startPosition + TravelDistance * ParallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, _startZ);
    }
}
