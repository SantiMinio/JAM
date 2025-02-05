using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCondition_Trigger : ActiveCondition
{
    [SerializeField] TriggerInteract trigger;

    [SerializeField] bool triggerDeactivate;

    [SerializeField] bool needToStay;

    [SerializeField] float timeToStay;
    [SerializeField] float timeToActivate = 1;

    bool stayed = false;

    string stayedTimer;

    private void Start()
    {
        trigger.OnColliderEnter += OnEnter;
        trigger.OnColliderExit += OnExit;
    }

    void OnEnter(Collider col)
    {
        if (stayed) return;
        stayed = true;

        if (needToStay)
        {
            stayedTimer = TimerManager.Instance.AddTimer(timeToStay, () => { }, Activate);
        }
        else
        {
            TimerManager.Instance.AddTimer(timeToActivate, () => { }, Activate);
            Debug.Log("pongo timer");
        }
    
    }

    void OnExit(Collider col)
    {
        if(!stayed) return;
        stayed = false;

        if (needToStay && stayedTimer != null)
        {
            TimerManager.Instance.StopTimer(stayedTimer, false);
        }

        if(activable.isActive &&  triggerDeactivate)
        {
            Deactivate();
        }
    }


    protected override void OnActivate()
    {
        stayedTimer = null;

        if (triggerDeactivate && !stayed)
        {
            Deactivate();
        }
        Debug.Log("activado");
    }

    protected override void OnDeactivate()
    {
        Debug.Log("desactivado");
    }
}
