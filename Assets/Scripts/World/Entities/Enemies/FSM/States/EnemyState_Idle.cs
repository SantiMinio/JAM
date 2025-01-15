using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class EnemyState_Idle : MonoBaseState
{
    [SerializeField] bool canRotate = true;
    [SerializeField] Enemy enemy = null;
    [SerializeField] MovementComponent moveComp = null;
    [SerializeField] string idleAnim = "Idle";

    public override void Enter(IState from, Dictionary<StateMachineInputs, object> transitionParameters = null)
    {
        base.Enter(from, transitionParameters);
        enemy.SetAnimation(idleAnim);

    }

    public override void UpdateLoop()
    {
        if (canRotate)
        {
            moveComp.SetAxisX(enemy.DirToTarget.x);
            moveComp.SetAxisY(enemy.DirToTarget.z);
            moveComp.Rotate();
        }
    }


    public override IState ProcessInput()
    {
        return this;
    }
}
