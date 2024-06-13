using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnightLogic : MonoBehaviour
{

    [SerializeField] private float walkSpeed = 5f;
    Rigidbody2D rigidBody;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start() {
        rigidBody.velocity = new Vector2(walkSpeed, rigidBody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
