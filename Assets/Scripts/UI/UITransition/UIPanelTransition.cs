using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class UIPanelTransition : MonoBehaviour
{
    [SerializeField] bool startOpen;

    [SerializeField] UnityEvent OpenEvent = new UnityEvent();
    [SerializeField] UnityEvent CloseEvent = new UnityEvent();
    [SerializeField] UnityEvent OpenOverEvent = new UnityEvent();
    [SerializeField] UnityEvent CloseOverEvent = new UnityEvent();
    
    
    public event Action OnCloseOver;
    public event Action OnOpenOver;
    public bool IsOpen { get => open; }
    
    bool open;


     public bool transition;

    private void Start()
    {
        Initialize();

        if (startOpen) OnOpenOverAbs();
        else OnCloseOverAbs();

        open = startOpen;
    }

    public void Open()
    {
        if (open) return;

        if (transition) CloseOver();

        open = true;
        OnOpenAbs();
        OpenEvent?.Invoke();
        transition = true;
    }

    public void Close()
    {
        if (!open) return;

        if (transition) OpenOver();

        open = false;
        OnCloseAbs();
        CloseEvent.Invoke();
        transition = true;
    }

    public void Closing(float deltaTime)
    {
        OnClosingAbs(deltaTime);
    }

    public void Opening(float deltaTime)
    {
        OnOpeningAbs(deltaTime);
    }

    protected void CloseOver()
    {
        transition = false;
        OnCloseOverAbs();
        OnCloseOver?.Invoke();
        CloseOverEvent.Invoke();
    }

    protected void OpenOver()
    {
        transition = false;
        OnOpenOverAbs();
        OnOpenOver?.Invoke();
        OpenOverEvent?.Invoke();
    }

    private void Update()
    {
        if (transition)
        {
            if (open)
                Opening(Time.deltaTime);
            else
                Closing(Time.deltaTime);
        }
    }

    public void Togle()
    {
        if (open)
            Close();
        else
            Open();
    }

    protected abstract void OnCloseAbs();
    protected abstract void OnOpenAbs();
    protected abstract void OnClosingAbs(float deltaTime);
    protected abstract void OnOpeningAbs(float deltaTime);
    protected abstract void OnOpenOverAbs();
    protected abstract void OnCloseOverAbs();

    protected abstract void Initialize();
}
