using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTarget : Hiteable
{
    private AnimateObjectActivator _activator;
    
    public bool rayOnMe = false;

    private float lastTimeRayHitted;
    
    public override void Start()
    {
        base.Start();
        _activator = GetComponent<AnimateObjectActivator>();

        onHit += AnimateObject;
    }

    void AnimateObject()
    {
        StopAllCoroutines();
        _activator.ActivateObject();

        rayOnMe = true;

        lastTimeRayHitted = 0;
        
        StartCoroutine(IsStillRayOn());
    }

    IEnumerator IsStillRayOn()
    {
        do
        {
            lastTimeRayHitted += Time.deltaTime;
            yield return new WaitForEndOfFrame();    
        } while (lastTimeRayHitted < 1);
        
        rayOnMe = false;
        _activator.DeactivateObject();
    }
}
