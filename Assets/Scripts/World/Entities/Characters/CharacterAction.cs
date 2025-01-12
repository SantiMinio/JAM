using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CharacterAction : MonoBehaviour, IPause
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
        if (startCooldown) return;
        OnActionStart();
        OnStartAction();
        actioning = true;
    }

    public void KeepAction()
    {
        if (!actioning || paused) return;
        OnActionKeep();
        OnKeepAction();
    }

    public void EndAction()
    {
        if (!actioning) return;
        OnActionEnd();
        OnEndAction();
        actioning = false;
        startCooldown = true;
    }

    private void Start()
    {
        PauseManager.instance.AddToPause(this);
    }

    private void Update()
    {
        if(paused) return;
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

    bool paused;
    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }
}
