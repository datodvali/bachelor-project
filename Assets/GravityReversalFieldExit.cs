using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityReversalFieldExit : MonoBehaviour
{
    private GravityReversalFieldScript _gravityReversalField;

    void Awake() {
        _gravityReversalField = gameObject.GetComponentInParent<GravityReversalFieldScript>();
        if (_gravityReversalField == null) {
            Debug.LogError("Gravity reversal field exit script used outside of its parent");
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        GameObject collidingObject = collider.gameObject;
        if (collidingObject.TryGetComponent<TouchDirections>(out var touchDirections)
            && collidingObject.TryGetComponent<Rigidbody2D>(out var rb)) {
            _gravityReversalField.RemoveObjectFromField(collidingObject);
        }
    }
}
