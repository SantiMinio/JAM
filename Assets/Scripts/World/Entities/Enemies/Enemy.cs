using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField] ParticleSystem particle_TakeDamage;
    [SerializeField] ParticleSystem particle_Death;
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
        ParticlesManager.Instance.GetParticlePool(particle_TakeDamage.name, particle_TakeDamage, 3);
        ParticlesManager.Instance.GetParticlePool(particle_Death.name, particle_Death, 3);
        brain.Initialize();
    }

    protected override void OnUpdate()
    {
        targetDetector.OnUpdate();
    }

    protected override void TakeDamage(Damager dmg)
    {
        ParticlesManager.Instance.PlayParticle(particle_TakeDamage.name, transform.position);
    }

    protected override void OnDeath()
    {
        ParticlesManager.Instance.PlayParticle(particle_Death.name, transform.position);
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
