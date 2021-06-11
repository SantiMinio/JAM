using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : ActivableBase, IActivable
{
    [SerializeField] private Animator _anim;

    //[SerializeField] private Image fillImageFeedback;
    
    [SerializeField] private float time;

    private bool open;

    private float _count;
    private void Update()
    {
        if (!IsActive())
        {
            if(open)
                Close();
            return;
        }

        //HandleFeedbackBar();
        
        if(open) return;
        
        _count += Time.deltaTime;

        
        
        if (_count >= time)
        {
            Open();
        }
    }

    void Open()
    {
        open = true;
        _anim.Play("open");
    }
    
    void Close()
    {
        _count = 0;
        open = false;
        _anim.Play("close");
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
