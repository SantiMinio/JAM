using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCondition_Timer : ActiveCondition
{
    [SerializeField] float startSecond = 0;

    [SerializeField] float timeToActive = 2;
    [SerializeField] float timeToDeactive = 2;

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
        TimerManager.Instance.AddTimer(timeToActive, () => { }, Activate);
    }
}
