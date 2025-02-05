using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveCondition : MonoBehaviour
{
    protected ActivableBase activable;

    private void Awake()
    {
        activable = GetComponent<ActivableBase>();
    }

    protected void Activate()
    {
        activable.Activate();
        OnActivate();
    }

    protected void Deactivate()
    {
        activable.Deactivate();
        OnDeactivate();
    }

    protected abstract void OnActivate();

    protected abstract void OnDeactivate();
}
