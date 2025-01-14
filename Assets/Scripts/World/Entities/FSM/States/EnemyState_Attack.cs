using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//public class EnemyState_Attack : MonoBaseState
//{
//    [SerializeField] Enemy owner = null;
//    [SerializeField] MovementComponent moveComp = null;
//    [SerializeField] EnemyAttack enemyAttack = null;

//    [SerializeField] float prepareTime = 0.5f;
//    [SerializeField] float recallTime = 1;
//    [SerializeField] bool canRotate = true;

//    float timer;

//    bool preparing;

//    public override void Enter(IState from, Dictionary<StateMachineInputs, object> transitionParameters = null)
//    {
//        base.Enter(from, transitionParameters);
//        timer = 0;
//        preparing = true;
//        enemyAttack.PrepareAttack();
//    }

//    public override void UpdateLoop()
//    {
//        timer += Time.deltaTime;

//        if (canRotate)
//        {
//            moveComp.SetAxisX(owner.DirToTarget.x);
//            moveComp.SetAxisY(owner.DirToTarget.z);
//            moveComp.Rotate();
//        }

//        if (preparing)
//        {
//            if(timer >= prepareTime)
//            {
//                timer = 0;
//                preparing = false;
//                enemyAttack.DoAttack();
//            }    
//        }
//        else
//        {
//            if(timer >= recallTime)
//            {
//                enemyAttack.AttackOver();
//            }
//        }
//    }

//    public override Dictionary<StateMachineInputs, object> Exit(IState to)
//    {
//        timer = 0;
//        return base.Exit(to);
//    }

//    public override IState ProcessInput()
//    {
//        return this;
//    }
//}
