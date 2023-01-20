using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlanet : MonoBehaviour
{
    [SerializeField] private float planetMoveSpeed = 1f;
    [SerializeField] private float planetRotationSpeed = 1f;
    
    private Rigidbody rb;

    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        rb.mass = 100;
        rb.useGravity = false;
        rb.velocity = new Vector2(-planetMoveSpeed, 0);
        rb.AddTorque(0, planetRotationSpeed, 0);
    }
}
