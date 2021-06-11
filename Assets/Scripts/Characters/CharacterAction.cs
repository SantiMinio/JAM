using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CharacterAction : MonoBehaviour
{
    public Action OnActionStart = delegate { };
    public Action OnActionKeep = delegate { };
    public Action OnActionEnd = delegate { };

    public void StartAction()
    {
        OnActionStart();
        OnStartAction();
    }

    public void KeepAction()
    {
        OnActionKeep();
        OnKeepAction();
    }

    public void EndAction()
    {
        OnActionEnd();
        OnEndAction();
    }

    protected abstract void OnStartAction();
    protected abstract void OnKeepAction();
    protected abstract void OnEndAction();
}
