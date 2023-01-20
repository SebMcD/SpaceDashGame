using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObject : MonoBehaviour
{
    [SerializeField] private Vector3 coordinatesToReach = new(0,0,0);

    void Update()
    {
        if(transform.position.x <= coordinatesToReach.x)
        {
            gameObject.SetActive(false);
        }
    }
}
