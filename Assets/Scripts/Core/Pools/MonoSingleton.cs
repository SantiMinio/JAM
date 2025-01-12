using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T owninstance;
    public static T Instance
    {
        get
        {
            return owninstance;
        }
    }
    void Awake()
    {
        if (owninstance == null)
        {
            owninstance = (T)this;
            OnAwake();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    protected abstract void OnAwake();
}
