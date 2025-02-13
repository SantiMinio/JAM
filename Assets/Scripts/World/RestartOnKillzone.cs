using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOnKillzone : MonoBehaviour
{
    Vector3 startPos;
    Vector3 startRotation;

    private void Start()
    {
        startPos = transform.position;
        startRotation = transform.eulerAngles;
    }

    public void RestartObject()
    {
        transform.position = startPos;
        transform.eulerAngles = startRotation;

        var rb = GetComponent<Rigidbody>();
        if(rb != null) 
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
