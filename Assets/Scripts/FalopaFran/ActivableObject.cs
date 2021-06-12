using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivableObject : ActivableBase
{
    [SerializeField] private Animator _anim;

    [SerializeField] private bool stayActivated;

    [SerializeField] private float time;

    public bool open;

    protected float _count;
    private void Update()
    {
        if(stayActivated && open) return;

        if (!isActive)
        {
            _count = 0;
            if (open)
            {
                Off();
            }
            return;
        }
        
        if(open) return;
        
        _count += Time.deltaTime;
        
        if (_count >= time)
        {
            On();
        }
    }
    protected virtual void On()
    {
        Debug.Log("asdasdasd");
        open = true;
        _anim.Play("on");
    }
    
    protected virtual void Off()
    {
        _count = 0;
        open = false;
        _anim.Play("off");
    }
}
