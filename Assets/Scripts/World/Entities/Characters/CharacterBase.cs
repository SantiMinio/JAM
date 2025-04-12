using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBase : Entity
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] CharacterAction action = null;
    [SerializeField] Animator anim = null;
    [SerializeField] MovementComponent moveComp = null;
    [SerializeField] PhysicsController physics = null;
    [SerializeField] Interactor interactor = null;
    [SerializeField] GrabberComponent grabComp = null;

    [SerializeField] string characterFallingSound = "Character_Falling";
    [SerializeField] string characterFallInLavaSound = "Character_FallInLava";
    [SerializeField] string characterGetHitSound = "Character_GetHit";
    [SerializeField] string characterStepsSound = "Character_Steps";
    [SerializeField] string characterCrySound = "Character_MaleCry";


    [SerializeField] float speed = 5;
    float stepTimer;


    public Vector3 CurrentDir => moveComp.Dir;


  
    #region Inputs
    public void MoveY(InputAction.CallbackContext callback)
    {
        if (IsDead || endGame) return;
        moveComp.SetAxisY(callback.ReadValue<float>());
    }

    public void MoveX(InputAction.CallbackContext callback)
    {
        if (IsDead || endGame) return;
        moveComp.SetAxisX(callback.ReadValue<float>());
    }

    public void ActionAbility(InputAction.CallbackContext callback)
    {
        if (grabComp.HaveGrabbedObject() || IsDead || endGame) return;

        switch (callback.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                action.StartAction();
                break;
            case InputActionPhase.Performed:
                action.KeepAction();
                break;
            case InputActionPhase.Canceled:
                action.EndAction();
                break;
        }
    }

    public void Interact(InputAction.CallbackContext callback)
    {
        if (callback.phase != InputActionPhase.Started || IsDead || endGame) return;


        if (grabComp.HaveGrabbedObject())
            grabComp.ThrowObject();
        else
            interactor.Interact();
    }
    #endregion

    protected override void OnEndGame()
    {
        base.OnEndGame();
        physics.StopPhysics();
    }

    public void DeadBySeparate()
    {
        anim.SetBool("Dead", true);
        IsDead = true;
    }

    public void Cry()
    {
        //inserte llanto
        SoundFX.PlaySound(characterCrySound);
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
        physics.StopPhysics();
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