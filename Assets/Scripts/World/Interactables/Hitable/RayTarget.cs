using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTarget : Hiteable
{
    private AnimateObjectActivator _activator;
    
    public bool rayOnMe = false;

    private float lastTimeRayHitted;
    

    protected override void OnInitialize()
    {
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
            if (paused)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }
            lastTimeRayHitted += Time.deltaTime;
            yield return new WaitForEndOfFrame();    
        } while (lastTimeRayHitted < 1);
        
        rayOnMe = false;
        _activator.DeactivateObject();
    }

}
