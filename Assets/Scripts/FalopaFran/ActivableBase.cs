using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableBase : MonoBehaviour, IActivable
{
    public bool isActive;
    public virtual bool IsActive()
    {
        return isActive;
    }

    public virtual void Activate()
    {
        isActive = true;
    }

    public virtual void Deactivate()
    {
        isActive = false;
    }
}
