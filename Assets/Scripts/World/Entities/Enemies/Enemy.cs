using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField] protected FSMBrain brain;
    [SerializeField] protected TargetDetector targetDetector;
    [SerializeField] protected PhysicsController physics;
    public Vector3 DirToTarget => targetDetector.CurrentTarget == null? Vector3.zero : (targetDetector.CurrentTarget.position - transform.position).normalized;

    public void SetAnimation(string anim)
    {

    }

    public void Despawn()
    {
        TurnOff();
    }

    protected override void OnTurnOff()
    {

        Destroy(this.gameObject);
    }

    protected override void OnTurnOn()
    {
        brain.ActivateBrain();
    }

    protected override void OnInitialize()
    {
        targetDetector.OnGetNewTarget += OnGetNewTarget;
        targetDetector.OnLostTarget += OnLostTarget;

        brain.Initialize();
    }

    protected override void OnUpdate()
    {
        targetDetector.OnUpdate();
    }

    protected override void TakeDamage(Damager dmg)
    {

    }

    protected override void OnDeath()
    {
        brain.SendInput(StateMachineInputs.ToDeath);
    }

    protected virtual void OnGetNewTarget(Transform target)
    {

    }

    protected virtual void OnLostTarget()
    {

    }

    protected override void OnPause()
    {
        base.OnPause();
        brain.DesactivateBrain();
        physics.Pause();
    }

    protected override void OnResume()
    {
        base.OnResume();
        brain.ActivateBrain();
        physics.Resume();
    }

    protected override void OnEndGame()
    {
        base.OnEndGame();
        brain.DesactivateBrain();
        physics.Pause();
    }
}
