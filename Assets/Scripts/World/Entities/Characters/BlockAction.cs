using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockAction : CharacterAction
{
    [SerializeField] DamageReceiver hiteable = null;
    [SerializeField] float blockAngle = 90;
    [SerializeField] Collider blockCollider = null;
    [SerializeField] Animator anim = null;
    [SerializeField] AudioClip blockSound = null;
    [SerializeField] string shieldAriseSound = null;
    [SerializeField] MovementComponent movement = null;
    [SerializeField] float defensiveSpeed;
    bool isBlocking;

    public override void Initialize(CharacterBase _owner)
    {
        base.Initialize(_owner);
        hiteable.IsInvulnerable = Blocking;
    }

    protected override void OnEndAction()
    {
        isBlocking = false;
        blockCollider.enabled = false;
        anim.SetBool("blocking", false);
        movement.SetSpeed(movement.initialSpeed);
        blockCollider.gameObject.tag = "rayTarget";
    }

    protected override void OnKeepAction()
    {
    }

    protected override void OnStartAction()
    {
        SoundFX.PlaySound(shieldAriseSound);
        isBlocking = true;
        blockCollider.enabled = true;
        anim.SetBool("blocking", true);
        Debug.Log("Blockea");
        movement.SetSpeed(defensiveSpeed);

    }

    bool Blocking(Damager dmg)
    {
        if (isBlocking)
        {
            return true;
        }
        else
            return false;
    }
}
