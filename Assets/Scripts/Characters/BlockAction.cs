using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockAction : CharacterAction
{
    [SerializeField] WorldHittable hiteable = null;
    [SerializeField] float blockAngle = 90;
    bool isBlocking;

    private void Start()
    {
        hiteable.Block = Blocking;
    }

    protected override void OnEndAction()
    {
        isBlocking = false;
    }

    protected override void OnKeepAction()
    {
    }

    protected override void OnStartAction()
    {
        isBlocking = true;
    }

    bool Blocking(Vector3 attackDir)
    {
        if (isBlocking)
        {
            attackDir.Normalize();

            float blockRange = Vector3.Dot(Main.instance.GetWife().currentDir, attackDir);

            if (blockRange <= blockAngle)
                return true;
            else
                return false;
        }
        else
            return false;
    }
}
