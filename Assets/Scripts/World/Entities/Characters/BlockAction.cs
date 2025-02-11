using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockAction : CharacterAction
{
    [SerializeField] DamageReceiver hiteable = null;
    [SerializeField] float blockAngle = 90;
    [SerializeField] Transform blockCollider = null;
    [SerializeField] Animator anim = null;
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
        blockCollider.gameObject.SetActive(false);
        anim.SetBool("blocking", false);
        movement.SetSpeed(movement.initialSpeed);
    }

    protected override void OnKeepAction()
    {
        blockCollider.forward = owner.CurrentDir;
    }

    protected override void OnStartAction()
    {
        SoundFX.PlaySound(shieldAriseSound);
        isBlocking = true;
        blockCollider.gameObject.SetActive(true);
        anim.SetBool("blocking", true);
        Debug.Log("Blockea");
        movement.SetSpeed(defensiveSpeed);

    }

    bool Blocking(Damager dmg)
    {
        if (isBlocking)
        {
            var attackDir = dmg.knockbackModule.knockbackDir;
            attackDir.Normalize();

            float blockRange = Vector3.Dot(owner.CurrentDir, attackDir);
            if (blockRange <= blockAngle)
            {
                blockCollider.gameObject.tag = "Mirror";
                return true;
            }
            else
            {
                blockCollider.gameObject.tag = "";
                return false;
            }
        }
        else
            return false;
    }
}
