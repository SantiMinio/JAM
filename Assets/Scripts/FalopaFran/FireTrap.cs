using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : ActivableObject
{
    [SerializeField] private ParticleSystem feedbackParticles;

    protected override void On()
    {
        feedbackParticles.Play();
        open = true;
    }
    
    protected override void Off()
    {
        _count = 0;
        open = false;
    }
    
}
