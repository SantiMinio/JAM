using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] float distanceToTargeted = 15;
    [SerializeField] float distanceToLostTarget = 10;
    [SerializeField] LayerMask possibleTargetMask;

    public event Action<Transform> OnGetNewTarget;
    public event Action OnLostTarget;

    Transform target;
    public Transform CurrentTarget => target;

    bool turnOn;

    public void OnUpdate()
    {
        if(CurrentTarget!= null)
        {
            DetectIfLostTarget();
        }

        if (!turnOn) return;

        DetectTarget();
    }

    void DetectTarget()
    {
        var overlap = Physics.OverlapSphere(transform.position, distanceToTargeted, possibleTargetMask);

        Transform tempTarget = null;
        for (int i = 0; i < overlap.Length; i++)
        {
            if(tempTarget == null)
            {
                tempTarget = overlap[i].transform;
                continue;
            }

            if(Distance(tempTarget) > Distance(overlap[i].transform))
            {
                tempTarget = overlap[i].transform;
            }
        }

        if(tempTarget != null && tempTarget != target)
        {
            target = tempTarget;
            OnGetNewTarget?.Invoke(tempTarget);
        }
    }

    float Distance(Transform t) => Vector3.Distance(transform.position, t.position);

    void DetectIfLostTarget()
    {
        if(distanceToLostTarget <= Distance(target))
        {
            target = null;
            OnLostTarget?.Invoke();
        }
    }

    public void TurnOn()
    {
        turnOn = true;
    }

    public void TurnOff()
    {
        turnOn = false;
    }
}
