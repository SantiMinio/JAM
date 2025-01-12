using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockAction : CharacterAction
{
    [SerializeField] WorldHittable hiteable = null;
    [SerializeField] float blockAngle = 90;
    [SerializeField] Transform blockCollider = null;
    [SerializeField] Animator anim = null;
    [SerializeField] AudioClip blockSound = null;
    [SerializeField] string shieldAriseSound = null;
    bool isBlocking;

    private void Start()
    {
        hiteable.Block = Blocking;
    }

    protected override void OnEndAction()
    {
        isBlocking = false;
        blockCollider.gameObject.SetActive(false);
        anim.SetBool("blocking", false);
    }

    protected override void OnKeepAction()
    {
        blockCollider.forward = Main.instance.GetWife().currentDir;
    }

    protected override void OnStartAction()
    {
        SoundFX.PlaySound(shieldAriseSound);
        isBlocking = true;
        blockCollider.gameObject.SetActive(true);
        anim.SetBool("blocking", true);
    }

    bool Blocking(Vector3 attackDir)
    {
        if (isBlocking)
        {
            attackDir.Normalize();

            float blockRange = Vector3.Dot(Main.instance.GetWife().currentDir, attackDir);

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
