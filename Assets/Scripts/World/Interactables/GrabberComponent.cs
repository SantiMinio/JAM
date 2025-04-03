using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberComponent : MonoBehaviour
{
    Grabbable currentGrabbable;

    [SerializeField] Transform grabbablePosition = null;
    [SerializeField] MovementComponent moveComp = null;

    float dropForce = 1;
    float throwForce = 7;

    public bool GrabObject(Grabbable _currentGrabbable)
    {
        if (HaveGrabbedObject()) return false;

        currentGrabbable = _currentGrabbable;
        currentGrabbable.SetFreezedPosition(grabbablePosition);

        return true;
    }

    public void ThrowObject()
    {
        if (!HaveGrabbedObject()) return;
        Vector3 dir = (moveComp.Dir + Vector3.up).normalized;
        currentGrabbable.Throw(dir, moveComp.InputDir.magnitude <= 0.3f ? dropForce : throwForce);
        currentGrabbable = null;
    }

    public bool HaveGrabbedObject() => currentGrabbable != null;
}
