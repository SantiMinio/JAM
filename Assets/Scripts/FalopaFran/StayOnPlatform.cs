using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StayOnPlatform : MonoBehaviour, IInteractable
{
    [SerializeField] private LayerMask triggerLayers;
    [SerializeField] private ActivableBase objetctActivable;

    public bool objectOnTop;
    
    private void OnTriggerStay(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            objectOnTop = true;
            OnStay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            objectOnTop = false;
            objetctActivable.Deactivate();
        }
    }

    public void OnStay()
    {
        objetctActivable.Activate();
    }
}
