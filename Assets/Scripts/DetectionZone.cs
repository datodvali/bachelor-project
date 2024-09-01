using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemaining;
    public UnityEvent collidersRemaining; 

    internal List<Collider2D> detectedColliders = new();
    Collider2D col;

    void Awake() {
        col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D enteredCol) {
        detectedColliders.Add(enteredCol);
        
        collidersRemaining.Invoke();
    }

    void OnTriggerExit2D(Collider2D exitedCol) {
        detectedColliders.Remove(exitedCol);
        
        if (detectedColliders.Count == 0) {
            noCollidersRemaining.Invoke();
        }
    }
}
