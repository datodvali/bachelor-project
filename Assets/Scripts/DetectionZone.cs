using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    internal List<Collider2D> detectedColliders = new();

    void OnTriggerEnter2D(Collider2D enteredCol) {
        detectedColliders.Add(enteredCol);
    }

    void OnTriggerExit2D(Collider2D exitedCol) {
        detectedColliders.Remove(exitedCol);
    }
}
