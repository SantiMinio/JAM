using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class EnemyState_Death : MonoBaseState
{
    [SerializeField] Enemy owner;

    [SerializeField] string anim = "Death";
    [SerializeField] string deadSound = "EnemyDeath";
    [SerializeField] float timeToDespawn = 1.5f;

    float timer;

    public override void Enter(IState from, Dictionary<StateMachineInputs, object> transitionParameters = null)
    {
        base.Enter(from, transitionParameters);
        owner.SetAnimation(anim);
        SoundFX.PlaySound(deadSound, AudioManager.OverlapMode.DontDisturb);
        timer = 0;
    }

    public override void UpdateLoop()
    {
        timer += Time.deltaTime;

        if (timer >= timeToDespawn)
        {
            owner.Despawn();
            timer = 0;
        }
    }

    public override Dictionary<StateMachineInputs, object> Exit(IState to)
    {
        timer = 0;
        return base.Exit(to);
    }

    public override IState ProcessInput()
    {
        return this;
    }
}
