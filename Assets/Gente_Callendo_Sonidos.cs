using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gente_Callendo_Sonidos : MonoBehaviour
{
    public AudioSource[] audioSources;

    private void Awake()
    {        
        audioSources = GetComponentsInChildren<AudioSource>();
    }
    private void OnParticleCollision(GameObject other)
    {
        foreach (AudioSource sonidos in audioSources)
        {
            sonidos.Play();
        }        
    }    
}
