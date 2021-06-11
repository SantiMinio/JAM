using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] float speed = 5;

    float xAxis;
    float yAxis;

    private void Update()
    {
        Vector3 movement = new Vector3(xAxis * speed, 0, yAxis * speed);
        rb.velocity = movement;
    }

    public void MoveY(float y)
    {
        yAxis = y;
    }

    public void MoveX(float x)
    {
        xAxis = x;
    }
}
