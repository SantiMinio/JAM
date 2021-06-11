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

    [SerializeField] private Image fillImageFeedback;

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
        _count = 0;
        objetctActivable.Deactivate();
        fillImageFeedback.gameObject.SetActive(false);
    }

    public void OnStay()
    {
        fillImageFeedback.gameObject.SetActive(true);
        
        _count += Time.deltaTime;

        HandleFeedbackBar();
        
        if (_count >= timeToActivate)
        {
            objetctActivable.Activate();
            fillImageFeedback.gameObject.SetActive(false);
            fillImageFeedback.fillAmount = 0;
        }
    }

    void HandleFeedbackBar()
    {
        float percentFill = _count / timeToActivate;

        if (percentFill > 1) percentFill = 1;

        fillImageFeedback.fillAmount = percentFill;
    }

    public void OnExecute()
    {
        
    }
}
