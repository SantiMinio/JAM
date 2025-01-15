using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerInteract : MonoBehaviour
{
    public event Action<Collider> OnColliderEnter;
    public event Action<Collider> OnColliderExit;
    public Collider myCollider;
    [SerializeField] public LayerMask layerToEffect;

    public void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerToEffect) != 0)
        {
            OnColliderEnter?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerToEffect) != 0)
        {
            OnColliderExit?.Invoke(other);
        }
    }
}
