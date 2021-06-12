using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] float speed = 5;
    [SerializeField] CharacterAction action = null;
    [SerializeField] Animator anim = null;
    [SerializeField] WorldHittable hiteable = null;

    float xAxis;
    float yAxis;

    public Vector3 currentDir = new Vector3(0, 0, -1);

    private void Update()
    {
        
        Vector3 movement = new Vector3(xAxis * (speed / (Mathf.Abs(yAxis) + 1)), rb.velocity.y, yAxis * (speed / (Mathf.Abs(xAxis) + 1)));
        Debug.Log(movement);
        rb.velocity = movement;

        if (movement != Vector3.zero)
        {
            currentDir = new Vector3(xAxis, 0, yAxis);
            anim.SetFloat("SetXDir", xAxis);
            anim.SetFloat("SetZDir", yAxis);
        }

        anim.SetFloat("x", xAxis);
        anim.SetFloat("z", yAxis);
    }

    public void MoveY(float y)
    {
        if (y < -0.3f) y = -1;
        else if (y > 0.3f) y = 1;
        else y = 0;
        yAxis = y;
    }

    public void MoveX(float x)
    {
        if (x < -0.3f) x = -1;
        else if (x > 0.3f) x = 1;
        else x = 0;
        xAxis = x;
    }

    public void ActionAbility(KeyEventButon eventKey)
    {
        switch (eventKey)
        {
            case KeyEventButon.KeyDown:
                action.StartAction();
                break;
            case KeyEventButon.Key:
                action.KeepAction();
                break;
            case KeyEventButon.KeyUp:
                action.EndAction();
                break;
            default:
                break;
        }
    }
}
