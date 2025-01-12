using System;
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

    [SerializeField] string characterFallingSound = "Character_Falling";
    [SerializeField] string characterFallInLavaSound = "Character_FallInLava";
    [SerializeField] string characterGetHitSound = "Character_GetHit";
    [SerializeField] string characterStepsSound = "Character_Steps";

    float stepTimer;

    float xAxis;
    float yAxis;

    public Vector3 currentDir = new Vector3(0, 0, -1);

    private void Start()
    {
        hiteable.onDead += Dead;
    }

    private void Update()
    {
        if (dead) return;

        Vector3 movement = new Vector3(Mathf.Abs(yAxis) == 1 ? xAxis / 1.5f * speed : xAxis * speed,
                                       rb.velocity.y,
                                       Mathf.Abs(xAxis) == 1 ? yAxis / 1.5f * speed : yAxis * speed);
        rb.velocity = movement;
        stepTimer += Time.deltaTime;
        movement.y = 0;
        if (movement != Vector3.zero)
        {
            currentDir = new Vector3(xAxis, 0, yAxis);
            anim.SetFloat("SetXDir", xAxis);
            anim.SetFloat("SetZDir", yAxis);
            if (stepTimer > 0.2f)
            {
                stepTimer = 0;
                SoundFX.PlaySound(characterStepsSound, AudioManager.OverlapMode.DontDisturb);
            }
        }

        anim.SetFloat("x", xAxis);
        anim.SetFloat("z", yAxis);
    }

    public void MoveY(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        yAxis = callback.ReadValue<float>();
    }

    public void MoveX(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        xAxis = callback.ReadValue<float>();
    }

    public void ActionAbility(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        switch (callback.phase)
        {
            case UnityEngine.InputSystem.InputActionPhase.Disabled:
                break;
            case UnityEngine.InputSystem.InputActionPhase.Waiting:
                break;
            case UnityEngine.InputSystem.InputActionPhase.Started:
                action.StartAction();
                break;
            case UnityEngine.InputSystem.InputActionPhase.Performed:
                action.KeepAction();
                break;
            case UnityEngine.InputSystem.InputActionPhase.Canceled:
                action.EndAction();
                break;
        }
    }
        bool dead;

    void Dead()
    {
        var characters = Main.instance.GetCharacters();
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != this) characters[i].Cry();
        }
        dead = true;
        if (Physics.Raycast(transform.position, -transform.up, 1, 1 << 9))
        {
            anim.SetBool("Fall", true);
            SoundFX.PlaySound(characterFallingSound);
            SoundFX.PlaySound(characterFallInLavaSound);
        }
        else
        {
            anim.SetBool("Dead", true);
            SoundFX.PlaySound(characterGetHitSound);
        }
        Main.instance.eventManager.TriggerEvent(GameEvents.CharactersSeparate);
    }

    public void DeadBySeparate()
    {
        anim.SetBool("Dead", true);
        dead = true;
    }

    public void Cry()
    {
        //inserte llanto
        anim.SetBool("Cry", true);
    }
}