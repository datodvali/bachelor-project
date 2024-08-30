using System.Collections.Generic;
using UnityEngine;

public class GravityReversalFieldScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsInField;

    void OnEnable() {
        foreach (GameObject obj in _objectsInField) {
            if (obj.TryGetComponent<TouchDirections>(out var touchDirections)
                && obj.TryGetComponent<Rigidbody2D>(out var rb)) {
                AddReverseGravityEffects(touchDirections, rb);
            } else {
                Debug.Log("A GameObject inside GravityReversalField doesn't have touchDirections component");
            }
        }
    }

    void OnDisable() {
        foreach (GameObject obj in _objectsInField) {
            if (obj.TryGetComponent<TouchDirections>(out var touchDirections)
                && obj.TryGetComponent<Rigidbody2D>(out var rb)) {
                RemoveReverseGravityEffects(touchDirections, rb);
            } else {
                Debug.Log("A GameObject inside GravityReversalField doesn't have touchDirections component");
            }
        }
    }

    public void AddObjectToField(GameObject obj) {
        if (obj.TryGetComponent<TouchDirections>(out var touchDirections)
            && obj.TryGetComponent<Rigidbody2D>(out var rb)) {
            _objectsInField.Add(obj);
            AddReverseGravityEffects(touchDirections, rb);
        } else {
            Debug.LogWarning("Tried adding game object without rigid body or touch directions component to a gravity reversal field");
        }
    }

    public void RemoveObjectFromField(GameObject obj) { 
        if (_objectsInField.Contains(obj)) {
            _objectsInField.Remove(obj);
            RemoveReverseGravityEffects(obj.GetComponent<TouchDirections>(), obj.GetComponent<Rigidbody2D>());
        } else {
            Debug.LogWarning("Tried removing nonexistent game object from gravity reversal field");
        }
    }

    private void AddReverseGravityEffects(TouchDirections touchDirections, Rigidbody2D rb) {
        Vector3 scale = rb.transform.localScale; 
        if (scale.y > 0) {
            rb.transform.localScale = new(scale.x, scale.y * -1, scale.z);
            rb.gravityScale *= -1;
            touchDirections.GravityReversed = true;
        }
    }

    private void RemoveReverseGravityEffects(TouchDirections touchDirections, Rigidbody2D rb) {
        Vector3 scale = rb.transform.localScale; 
        if (scale.y < 0) {
            rb.transform.localScale = new(scale.x, scale.y * -1, scale.z);
            rb.gravityScale *= -1;
            touchDirections.GravityReversed = false;
        }
    }
}
