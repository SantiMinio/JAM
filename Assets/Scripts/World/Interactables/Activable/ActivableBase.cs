using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableBase : MonoBehaviour, IActivable, IPause
{
    public bool isActive;

    private void Start()
    {
        PauseManager.instance.AddToPause(this);
    }

    private void Update()
    {
        if (paused) return;
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {

    }

    public bool IsActive()
    {
        return isActive;
    }

    public void Activate()
    {
        isActive = true;
        OnActivate();
    }

    public virtual void Anticipation()
    {
    }

    public void Deactivate()
    {
        isActive = false;
        OnDeactivate();
    }

    protected virtual void OnActivate()
    {

    }

    protected virtual void OnDeactivate()
    {

    }

    protected bool paused;

    public virtual void Pause()
    {
        paused = true;
    }

    public virtual void Resume()
    {
        paused = false;
    }
}
