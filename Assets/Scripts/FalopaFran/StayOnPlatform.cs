using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StayOnPlatform : MonoBehaviour, IInteractable
{
    [SerializeField] private LayerMask triggerLayers;

    [SerializeField] private float timeToActivate;

    [SerializeField] private ActivableBase objetctActivable;

    

    private float _count;
    private void OnTriggerStay(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            OnStay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objetctActivable.Deactivate();
    }

    public void OnStay()
    {
        objetctActivable.Activate();
    }

    

    public void OnExecute()
    {
        
    }
}
