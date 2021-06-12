using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CharacterAction : MonoBehaviour
{
    public Action OnActionStart = delegate { };
    public Action OnActionKeep = delegate { };
    public Action OnActionEnd = delegate { };

    [SerializeField] float useCooldown = 2;
    float cooldown;
    bool startCooldown;
    bool actioning;

    public void StartAction()
    {
        Debug.Log("hmmmm");
        if (startCooldown) return;
        OnActionStart();
        OnStartAction();
        actioning = true;
    }

    public void KeepAction()
    {
        if (!actioning) return;
        OnActionKeep();
        OnKeepAction();
    }

    public void EndAction()
    {
        Debug.Log("?");
        if (!actioning) return;
        OnActionEnd();
        OnEndAction();
        actioning = false;
        startCooldown = true;
    }


    private void Update()
    {
        if (startCooldown)
        {
            cooldown += Time.deltaTime;
            if(cooldown >= useCooldown)
            {
                cooldown = 0;
                startCooldown = false;
            }    
        }
    }
    protected abstract void OnStartAction();
    protected abstract void OnKeepAction();
    protected abstract void OnEndAction();
}
