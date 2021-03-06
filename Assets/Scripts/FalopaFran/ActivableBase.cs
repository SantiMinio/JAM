using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableBase : MonoBehaviour, IActivable
{
    public bool isActive;
    public bool IsActive()
    {
        return isActive;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
