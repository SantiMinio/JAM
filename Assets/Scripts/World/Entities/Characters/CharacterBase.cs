using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : Entity
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] float speed = 5;
    [SerializeField] CharacterAction action = null;
    [SerializeField] Animator anim = null;
    [SerializeField] MovementComponent moveComp = null;
    [SerializeField] PhysicsController physics = null;

    [SerializeField] string characterFallingSound = "Character_Falling";
    [SerializeField] string characterFallInLavaSound = "Character_FallInLava";
    [SerializeField] string characterGetHitSound = "Character_GetHit";
    [SerializeField] string characterStepsSound = "Character_Steps";

    float stepTimer;


    public Vector3 CurrentDir => moveComp.Dir;

    #region Inputs
    public void MoveY(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        moveComp.SetAxisY(callback.ReadValue<float>());
    }

    public void MoveX(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        moveComp.SetAxisX(callback.ReadValue<float>());
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
    #endregion

    public void DeadBySeparate()
    {
        anim.SetBool("Dead", true);
        IsDead = true;
    }

    public void Cry()
    {
        //inserte llanto
        anim.SetBool("Cry", true);
    }
    float animSpeed;


    protected override void OnPause()
    {
        base.OnPause();
        animSpeed = anim.speed;
        anim.speed = 0;
        physics.Pause();
    }

    protected override void OnResume()
    {
        base.OnResume();
        paused = false;
        anim.speed = animSpeed;
        physics.Resume();
    }

    protected override void OnTurnOff()
    {
    }

    protected override void OnTurnOn()
    {
    }

    protected override void OnInitialize()
    {
        action.Initialize(this);
    }

    protected override void OnUpdate()
    {
        moveComp.Rotate();
        var moveDir = moveComp.Move();
        stepTimer += Time.deltaTime;

        if (moveDir != Vector3.zero)
        {
            anim.SetBool("Run", true);
            if (stepTimer > 0.2f)
            {
                stepTimer = 0;
                SoundFX.PlaySound(characterStepsSound, AudioManager.OverlapMode.DontDisturb);
            }
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    protected override void TakeDamage(Damager dmg)
    {
    }

    protected override void OnDeath()
    {
        var characters = Main.instance.GetCharacters();
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != this) characters[i].Cry();
        }
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
        TurnOff();
        Main.instance.eventManager.TriggerEvent(GameEvents.CharactersSeparate);
    }
}