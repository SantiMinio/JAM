using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivableBase, IActivable
{
    void Open()
    {
        gameObject.SetActive(false);
    }
    
    void Close()
    {
        gameObject.SetActive(true);
    }

    public override void Activate()
    {
        Open();
    }

    public override void Deactivate()
    {
        Close();
    }
}
