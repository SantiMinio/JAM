using System.Collections;
using System.Collections.Generic;
using DevelopTools;
using Tools.Sound;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPool : SingleObjectPool<AudioSource>
{
   public bool soundPoolPlaying = false;
   public AudioManager.SoundDimesion dimension;

    public AudioSource GetObject(AudioClip clip, AudioMixerGroup mixer, bool loop = false)
    {
        var audio = Get();
        audio.clip = clip;
        audio.outputAudioMixerGroup = mixer;
        audio.loop = loop;

        return audio;
    }

   protected override void AddObject(int prewarm = 3)
   {
      //var newAudio = ASourceCreator.Create2DSource(_audioClip, _audioClip.name, _audioMixer, _loop, playOnAwake);
      var newAudio = dimension == AudioManager.SoundDimesion.ThreeD? ASourceCreator.Create3DSource(null, name, null, false, false) :
            ASourceCreator.Create2DSource(gameObject, null, name, null, false, false);
        newAudio.gameObject.SetActive(false);
      newAudio.transform.SetParent(transform);
      objects.Enqueue(newAudio);
   }

    public void StopAllSounds()
    {
       for (int i = currentlyUsingObj.Count - 1; i >= 0; i--)
       {
          if (currentlyUsingObj[i].isPlaying)
          {
             currentlyUsingObj[i].Stop();
             ReturnToPool(currentlyUsingObj[i]);
          }
       }

       soundPoolPlaying = false;
    }

    public void PauseAudio()
    {
        for (int i = 0; i < currentlyUsingObj.Count; i++)
        {
            if (currentlyUsingObj[i] == null)
            {
                currentlyUsingObj.RemoveAt(i);
                i -= 1;
                continue;
            }

            currentlyUsingObj[i].Pause();
        }
    }

    public void ResumeAudio()
    {
        for (int i = 0; i < currentlyUsingObj.Count; i++)
        {
            if (currentlyUsingObj[i] == null)
            {
                currentlyUsingObj.RemoveAt(i);
                i -= 1;
                continue;
            }
            if (!currentlyUsingObj[i].isPlaying)
                currentlyUsingObj[i].Play();
        }
    }

    public bool IsThisObjectFromThisPool(AudioSource audioSource)
    {
        if (currentlyUsingObj.Contains(audioSource)) return true;
        else return false;
    }
}