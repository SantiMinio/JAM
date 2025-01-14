using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class EnemyState_Chase : MonoBaseState
{
    [SerializeField] Enemy enemy = null;
    [SerializeField] MovementComponent moveComp = null;
    [SerializeField] string moveAnim = "Move";
    [SerializeField] bool useObstacleAvoidance = true;

    public override void Enter(IState from, Dictionary<StateMachineInputs, object> transitionParameters = null)
    {
        base.Enter(from, transitionParameters);
        enemy.SetAnimation(moveAnim);
    }

    public override void UpdateLoop()
    {
        var moveDir = enemy.DirToTarget;

        if(useObstacleAvoidance)
            moveDir = moveComp.ObstacleAvoidance(moveDir);

        moveComp.SetAxisX(moveDir.x);
        moveComp.SetAxisY(moveDir.z);
        moveComp.Rotate();
        moveComp.Move();
    }

    public override Dictionary<StateMachineInputs, object> Exit(IState to)
    {
        moveComp.StopMove();
        return base.Exit(to);
    }

    public override IState ProcessInput()
    {
        return this;
    }
}
