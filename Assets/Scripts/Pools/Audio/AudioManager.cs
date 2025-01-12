using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] AudioMixerGroup mainMixer = null;
    public enum SoundDimesion { ThreeD, TwoD }
    public enum OverlapMode { None, DontDisturb, CancelCurrent }
    public enum SoundPriority { Medium = 75, High = 0, Low = 150 }

    private Dictionary<string, List<AudioSource>> _soundRegistry = new Dictionary<string, List<AudioSource>>();
    [SerializeField] int pool2DAmmount = 10;
    [SerializeField] int pool3DAmmount = 10;

    [SerializeField] AudioSource[] ambientalSounds = new AudioSource[0];

    SoundPool pool3D = null;
    SoundPool pool2D = null;

    protected override void OnAwake()
    {
        pool3D = new GameObject($"Sound Pool 3D").AddComponent<SoundPool>();
        pool3D.transform.SetParent(transform);
        pool3D.dimension = SoundDimesion.ThreeD;
        pool3D.Initialize(pool3DAmmount);

        pool2D = new GameObject($"Sound Pool 2D").AddComponent<SoundPool>();
        pool2D.transform.SetParent(transform);
        pool2D.dimension = SoundDimesion.TwoD;
        pool2D.Initialize(pool2DAmmount);
    }


    #region TO USE
    public AudioSource PlaySound(string soundPoolName, AudioClip clip, SoundDimesion dimension = SoundDimesion.TwoD, bool loop = false, OverlapMode overlapMode = OverlapMode.None, AudioMixerGroup overrideMixer = null, SoundPriority soundPriority = SoundPriority.Medium, Transform trackingTransform = null, Action calbackEnd = null)
    {
        if (overlapMode == OverlapMode.DontDisturb && (_soundRegistry.ContainsKey(soundPoolName) && _soundRegistry[soundPoolName].Count > 0)) return null;

        if(overlapMode == OverlapMode.CancelCurrent) 
            StopAllSounds(soundPoolName);

        var soundPool = dimension == SoundDimesion.TwoD ? pool2D : pool3D;

        AudioSource aS = soundPool.GetObject(clip, overrideMixer != null ? overrideMixer : mainMixer, loop);

        if (aS == null)
        {
            return null;
        }

        aS.priority = (int)soundPriority;
        soundPool.soundPoolPlaying = true;

        if (trackingTransform != null) aS.transform.position = trackingTransform.position;
        aS.Play();

        if (!aS.loop)
            StartCoroutine(ReturnSoundToPool(aS, soundPoolName, calbackEnd));


        if (_soundRegistry.ContainsKey(soundPoolName))
        {
            _soundRegistry[soundPoolName].Add(aS);
        }
        else
        {
            _soundRegistry.Add(soundPoolName, new List<AudioSource>() { aS });
        }

        return aS;
    }
    public void StopAllSounds(string soundPoolName = null)
    {
        if (soundPoolName == null)
        {
            pool2D.StopAllSounds();
            pool3D.StopAllSounds();
        }
        else if (_soundRegistry.ContainsKey(soundPoolName))
        {
            for (int i = 0; i < _soundRegistry[soundPoolName].Count; i++)
            {
                _soundRegistry[soundPoolName][i].Stop();
                if (pool2D.IsThisObjectFromThisPool(_soundRegistry[soundPoolName][i]))
                    pool2D.ReturnToPool(_soundRegistry[soundPoolName][i]);
                else if (pool3D.IsThisObjectFromThisPool(_soundRegistry[soundPoolName][i]))
                    pool3D.ReturnToPool(_soundRegistry[soundPoolName][i]);
            }

            _soundRegistry[soundPoolName].Clear();
        }
        else
        {
            Debug.LogWarning("No tenes ese sonido en en pool");
        }
    }

    public void StopSound(AudioSource source, string soundPoolName)
    {
        if (_soundRegistry.ContainsKey(soundPoolName) && _soundRegistry[soundPoolName].Contains(source))
        {
            source.Stop();

            if (pool2D.IsThisObjectFromThisPool(source))
                pool2D.ReturnToPool(source);
            else if (pool3D.IsThisObjectFromThisPool(source))
                pool3D.ReturnToPool(source);

            _soundRegistry[soundPoolName].Remove(source);
        }
    }

    public void PauseSounds()
    {
        pool3D.PauseAudio();
        pool2D.PauseAudio();
        for (int i = 0; i < ambientalSounds.Length; i++)
        {
            if (ambientalSounds[i].isPlaying)
                ambientalSounds[i].Pause();
        }
    }
    public void ResumeSounds()
    {
        pool3D.ResumeAudio();
        pool2D.ResumeAudio();

        for (int i = 0; i < ambientalSounds.Length; i++)
        {
            ambientalSounds[i].Play();
        }
    }
    #endregion

    #region Privates

    IEnumerator ReturnSoundToPool(AudioSource aS, string sT, Action EndCallback = null)
    {
        yield return new WaitForSeconds(aS.clip.length);

        EndCallback?.Invoke();
        if (pool2D.IsThisObjectFromThisPool(aS))
            pool2D.ReturnToPool(aS);
        else if (pool3D.IsThisObjectFromThisPool(aS))
            pool3D.ReturnToPool(aS);
        _soundRegistry[sT].Remove(aS);
    }
    #endregion


    public void DeleteSoundPool(string soundPoolName)
    {
        //Destroy(_soundRegistry[soundPoolName].gameObject);
        //_soundRegistry.Remove(soundPoolName);
    }
}
