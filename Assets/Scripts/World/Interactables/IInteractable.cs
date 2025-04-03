using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void OnInteract(Interactor interactor);
    public abstract void OnEnterInteract(Interactor interactor);
    public abstract void OnExitInteract(Interactor interactor);

    private void OnTriggerEnter(Collider other)
    {
        var collector = other.GetComponent<Interactor>();
        if (collector != null)
            collector.EnterCollect(this);
    }

    private void OnTriggerExit(Collider other)
    {
        var collector = other.GetComponent<Interactor>();
        if (collector != null)
            collector.ExitCollect(this);
    }
}
