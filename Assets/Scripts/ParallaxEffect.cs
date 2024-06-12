using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform target;

    Vector2 startPosition;
    float startZ;
    float distanceFromTarget => transform.position.z - target.position.z;
    float clippingPlane => cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane);
    float parallaxFactor => Mathf.Abs(distanceFromTarget) / clippingPlane;

    Vector2 travelDistance => (Vector2) cam.transform.position - startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;   
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPosition = startPosition + travelDistance * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
    }
}
