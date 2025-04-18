using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ActiveCondition_Timer : ActiveCondition
{
    [SerializeField] float startSecond = 0;

    [SerializeField] float timeToActive = 2;
    [SerializeField] float timeToDeactive = 2;

    [SerializeField] float anticipationTime = 0.5f;

    private void Start()
    {
        TimerManager.Instance.AddTimer(timeToActive - startSecond,()=> { }, Activate);
    }

    protected override void OnActivate()
    {
        TimerManager.Instance.AddTimer(timeToDeactive, () => { }, Deactivate);

    }

    protected override void OnDeactivate()
    {
        var anticipationTimer = timeToActive - anticipationTime;

        TimerManager.Instance.AddTimer(timeToActive, () => { }, Activate);
        TimerManager.Instance.AddTimer(anticipationTimer, () => { }, activable.Anticipation);
    }
}
