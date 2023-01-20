using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBGObject : MonoBehaviour
{
    [SerializeField] private Vector2 force = new(0,0);
    [SerializeField] private Vector3 pos;
    [SerializeField] private float xPosReached = 0f;

    private Rigidbody rb;

    void Start()
    {
        pos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.velocity = force;
    }

    void FixedUpdate()
    {
        if (transform.position.x < xPosReached)
        {
            transform.position = pos;
        }
    }
}
