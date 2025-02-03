using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayers;
    
    [SerializeField] private ParticleSystem feedbackTouchLava;

    [SerializeField] private AudioSource fryObjectSound;
    private void OnTriggerEnter(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
       //     feedbackTouchLava.transform.position =  other.transform.position;
            feedbackTouchLava.Play();
            
            fryObjectSound.Play();
            
            var posibleHitable = other.GetComponent<DamageReceiver>();

            if (posibleHitable != null)
            {
                posibleHitable.InstaKill();
            }
            
        } 
    }
}
