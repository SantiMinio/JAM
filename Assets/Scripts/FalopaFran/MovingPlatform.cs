using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : ActivableBase
{
    [SerializeField] private Transform objectToMove;

    [SerializeField] private Vector3 dir;

    [SerializeField] private float speed, time;

    private float _count;

    public override void Activate()
    {
        _count += Time.deltaTime;

        if (_count >= time)
        {
            
        }
    }

    public override void Deactivate()
    {
        
    }
}
