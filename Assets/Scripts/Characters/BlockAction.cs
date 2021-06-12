using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockAction : CharacterAction
{
    [SerializeField] WorldHittable hiteable = null;
    [SerializeField] float blockAngle = 90;
    [SerializeField] Transform blockCollider;
    bool isBlocking;

    private void Start()
    {
        hiteable.Block = Blocking;
    }

    protected override void OnEndAction()
    {
        isBlocking = false;
        blockCollider.gameObject.SetActive(false);
    }

    protected override void OnKeepAction()
    {
        blockCollider.forward = Main.instance.GetWife().currentDir;
    }

    protected override void OnStartAction()
    {
        isBlocking = true;
        blockCollider.gameObject.SetActive(true);
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
