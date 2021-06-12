using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTarget : Hiteable
{
    private AnimateObjectActivator _activator;
    
    bool rayOnMe = false;
    
    public override void Start()
    {
        base.Start();
        _activator = GetComponent<AnimateObjectActivator>();

        onHit += AnimateObject;
    }

    void AnimateObject()
    {
        _activator.ActivateObject();

        rayOnMe = true;

    }

    private void Update()
    {
        if(!rayOnMe) _activator.DeactivateObject();
    }

    private void LateUpdate()
    {
        rayOnMe = false;
    }
}
