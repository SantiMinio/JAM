using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chaser : Enemy
{
    [SerializeField] Damager dmg;
    [SerializeField] TriggerInteract damageTrigger;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        damageTrigger.OnColliderEnter += OnDamage;
    }

    protected override void OnTurnOn()
    {
        base.OnTurnOn();
        targetDetector.TurnOn();
    }

    protected override void OnGetNewTarget(Transform target)
    {
        base.OnGetNewTarget(target);
        Debug.Log("conseguí un nuevo target");
        brain.SendInput(FSM.StateMachineInputs.ToMove);
        targetDetector.TurnOff();
    }

    protected override void OnLostTarget()
    {
        base.OnLostTarget();
        brain.SendInput(FSM.StateMachineInputs.ToIdle);
        targetDetector.TurnOn();
    }

    void OnDamage(Collider col)
    {
        if (col.GetComponent<DamageReceiver>())
            col.GetComponent<DamageReceiver>().DoDamage(dmg);
    }
}
