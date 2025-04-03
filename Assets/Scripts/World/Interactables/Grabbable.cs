using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Grabbable : Interactable
{
    [SerializeField] string getMessage = null;
    public bool autoCollect = true;
    [SerializeField] Collider trigger = null;
    [SerializeField] Rigidbody rb = null;

    protected virtual void Start()
    {

    }


    public override void OnInteract(Interactor collector)
    {
        var grabber = collector.GetComponent<GrabberComponent>();
        if (grabber != null)
            grabber.GrabObject(this);
    }

    public override void OnEnterInteract(Interactor collector)
    {
    }

    public override void OnExitInteract(Interactor collector)
    {
    }

    public void SetFreezedPosition(Transform position)
    {
        trigger.enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = position.position;
        transform.rotation = position.rotation;
        transform.parent = position;
    }

    public void Throw(Vector3 dir, float force)
    {
        transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(dir * force, ForceMode.Impulse);
        trigger.enabled = true;
    }

    protected virtual void OnTurnOff()
    {

    }
}
