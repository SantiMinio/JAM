using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Audio;

public class SoundFX : MonoSingleton<SoundFX>
{
    [SerializeField] SoundProperties[] sounds = new SoundProperties[0];
    protected override void OnAwake()
    {
    }
    public static AudioSource PlaySound(string ID, AudioManager.OverlapMode overlap = AudioManager.OverlapMode.None)
    {
        if (ID == null) return null;

        var properties = Instance.SearchByID(ID);

        if(properties.sounds == null || properties.sounds.Length <= 0)
        {
            Debug.LogWarning("No tenes seteado el sonido: " + ID);
            return null;
        }

        Debug.LogWarning("sound " + ID);
        int currentIndex = Random.Range(0, properties.sounds.Length);
        if (properties.isSequencial)
        {
            currentIndex = properties.currentIndex;
            properties.IncrementIndex();
            Instance.ChangeByID(properties);
        }

        return AudioManager.Instance.PlaySound(properties.ID, properties.sounds[currentIndex], properties.dimension, properties.isLoop, overlap, properties.group, properties.priority);
    }

    public static void StopSound(string ID)
    {
        AudioManager.Instance.StopAllSounds(ID);
    }

    public SoundProperties SearchByID(string ID)
    {
        SoundProperties result = new SoundProperties();
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ID == ID)
            {
                result = sounds[i];
                break;
            }
        }

        return result;
    }

    public void ChangeByID(SoundProperties property)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ID == property.ID)
            {
                sounds[i] = property;
                break;
            }
        }
    }
}

[System.Serializable]
public struct SoundProperties
{
    public string ID;
    public AudioClip[] sounds;
    [HideInInspector] public int currentIndex;
    public AudioManager.SoundDimesion dimension;
    public AudioManager.SoundPriority priority;
    public bool isLoop;
    public bool isSequencial;
    [SerializeField] internal AudioMixerGroup group;
    

    public void IncrementIndex()
    {
        currentIndex += 1;
        if(currentIndex >= sounds.Length)
        {
            currentIndex = 0;
        }
    }
}
