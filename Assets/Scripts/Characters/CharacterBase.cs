using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] float speed = 5;
    [SerializeField] CharacterAction action = null;
    [SerializeField] Animator anim;

    float xAxis;
    float yAxis;

    private void Update()
    {
        Vector3 movement = new Vector3(xAxis * speed, 0, yAxis * speed);
        rb.velocity = movement;

        anim.SetFloat("x", xAxis);
        anim.SetFloat("z", yAxis);
    }

    public void MoveY(float y)
    {
        yAxis = y;
    }

    public void MoveX(float x)
    {
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
