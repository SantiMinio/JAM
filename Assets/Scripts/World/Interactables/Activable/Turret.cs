using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ActivableBase
{
    public float xAngle, yAngle, zAngle;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (!isActive) return;

        transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
    }
}
