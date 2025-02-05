using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivablePlatform : ActivableBase
{
    [SerializeField] Transform onPos = null;
    [SerializeField] Transform offPos = null;
    [SerializeField] float moveSpeed = 5;

    [SerializeField] Transform platformModel = null;

    private void Start()
    {
        platformModel.position = offPos.position;
    }

    protected override void OnActivate()
    {
        base.OnActivate();
        StartMove(onPos.position);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (moving)
        {
            timer += Time.deltaTime;

            platformModel.transform.position = Vector3.Lerp(initPos, finalPos, timer / timeToGo);

            if(timer >= timeToGo)
            {
                FinishMove();
            }
        }
    }

    bool moving;
    float timer;
    float timeToGo;
    Vector3 initPos;
    Vector3 finalPos;

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
        StartMove(offPos.position);

    }

    void StartMove(Vector3 posToGo)
    {
        moving = true;
        timer = 0;
        timeToGo = (platformModel.position - posToGo).magnitude / moveSpeed;
        initPos = platformModel.position;
        finalPos = posToGo;
    }

    void FinishMove()
    {
        timer = 0;
        moving = false;
        platformModel.position = finalPos;
    }
}
