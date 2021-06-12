using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayers;
    private void OnTriggerEnter(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            var posibleHitable = other.GetComponent<IHiteable>();

            if (posibleHitable != null)
            {
                posibleHitable.GetHit(Vector3.down);
            }
            
        } 
    }
}
