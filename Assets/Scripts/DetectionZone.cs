using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemaining; 

    internal List<Collider2D> detectedColliders = new();
    Collider2D col;

    void Awake() {
        col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D enteredCol) {
        detectedColliders.Add(enteredCol);
    }

    void OnTriggerExit2D(Collider2D exitedCol) {
        detectedColliders.Remove(exitedCol);
        
        if (detectedColliders.Count == 0) {
            noCollidersRemaining.Invoke();
        }
    }
}
