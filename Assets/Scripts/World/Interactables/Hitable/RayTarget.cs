using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTarget : Hiteable, IPause
{
    private AnimateObjectActivator _activator;
    
    public bool rayOnMe = false;

    private float lastTimeRayHitted;
    
    public override void Start()
    {
        base.Start();
        _activator = GetComponent<AnimateObjectActivator>();
        PauseManager.instance.AddToPause(this);
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

    bool paused;

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }
}
