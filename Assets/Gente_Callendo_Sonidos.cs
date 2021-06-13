using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gente_Callendo_Sonidos : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;

    private void Awake()
    {        
        audioSource = GetComponent<AudioSource>();
        audioSource2 = GetComponent<AudioSource>();
    }
    private void OnParticleCollision(GameObject other)
    {
        audioSource.Play();
        audioSource2.Play();              
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        audioSource2.Play();
    }
}
