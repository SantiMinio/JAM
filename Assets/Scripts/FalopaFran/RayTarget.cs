using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTarget : Hiteable, IActivable
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
