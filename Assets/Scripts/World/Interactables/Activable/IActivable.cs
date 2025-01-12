using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivable
{
    bool IsActive();
    void Activate();
    void Deactivate();
}
