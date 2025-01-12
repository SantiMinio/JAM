using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivableObject : ActivableBase, IPause
{
    [SerializeField] private Animator _anim;

    [SerializeField] private bool stayActivated;

    [SerializeField] private float time;

    public bool open;

    protected float _count;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if ((stayActivated && open)) return;

        if (!isActive)
        {
            _count = 0;
            if (open)
            {
                Off();
            }
            return;
        }

        if (open) return;

        _count += Time.deltaTime;

        if (_count >= time)
        {
            On();
        }
    }

    protected virtual void On()
    {
        open = true;
        _anim.Play("on");
    }
    
    protected virtual void Off()
    {
        _count = 0;
        open = false;
        _anim.Play("off");
    }

    float animSpeed;

    public override void Pause()
    {
        base.Pause();
        if (_anim == null) return;
        animSpeed = _anim.speed;
        _anim.speed = 0;
    }

    public override void Resume()
    {
        base.Resume();
        if (_anim == null) return;

        _anim.speed = animSpeed;
    }
}
