using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedObject : ActivableBase, IActivable
{
    [SerializeField] private Animator _anim;

    //[SerializeField] private Image fillImageFeedback;

    [SerializeField] private bool stayActivated;
    
    [SerializeField] private float timeToTurnOff;
    
    [SerializeField] private float time;

    private bool open;

    private float _count;
    private void Update()
    {
        if(stayActivated && open) return;
        
        
        if (!IsActive())
        {
            if(open)
                Off();
            return;
        }

        //HandleFeedbackBar();
        
        if(open) return;
        
        _count += Time.deltaTime;

        
        
        if (_count >= time)
        {
            On();
        }
    }

    IEnumerator TimeToClose()
    {
        yield return new WaitForSeconds(timeToTurnOff);
        
        Off();
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
    
    // void HandleFeedbackBar()
    // {
    //     float percentFill = _count / time;
    //
    //     if (percentFill > 1) percentFill = 1;
    //
    //     fillImageFeedback.fillAmount = percentFill;
    // }
}
