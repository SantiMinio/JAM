using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCircuit : ActivableBase
{
    [SerializeField] private List<StayOnPlatform> platforms = new List<StayOnPlatform>();

    public bool allTrue;

    private AnimateObjectActivator _animateObjectActivator;


    private void Start()
    {
        _animateObjectActivator = GetComponent<AnimateObjectActivator>();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        allTrue = CheckCircuit();

        if (allTrue) _animateObjectActivator.ActivateObject();
        else
        {
            _animateObjectActivator.DeactivateObject();
        }
    }

    private bool CheckCircuit()
    {
        foreach (var platform in platforms)
        {
            if (!platform.objectOnTop) return false;
        }
        return true;
    }
}
