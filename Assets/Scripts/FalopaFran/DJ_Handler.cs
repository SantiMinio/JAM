using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJ_Handler : MonoBehaviour
{
    public List<AudioSource> musicas = new List<AudioSource>();

    public AudioSource _currentAudioSource;

    private int currentAudioIndex;

    private void Start()
    {
        _currentAudioSource = musicas[currentAudioIndex];
        
        _currentAudioSource.Play();
    }
    public void ChangeMusic(int index)
    {
        _currentAudioSource.Stop();
        _currentAudioSource = musicas[index];
        _currentAudioSource.Play();
    }
    
}
