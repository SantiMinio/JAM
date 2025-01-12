﻿using UnityEngine.Audio;

namespace Tools.Sound
{
    using UnityEngine;

    public static class ASourceCreator
    {
        public static AudioSource Create2DSource(GameObject parent, AudioClip ac, string name, AudioMixerGroup mixerGroup, bool loop = false, bool playOnAwake = false)
        {
            Transform cam = AudioManager.Instance.transform;

            var source = cam
                .gameObject
                .CreateDefaultSubObject<AudioSource>("SOURCE-> " + name);

            source.outputAudioMixerGroup = mixerGroup;
            source.clip = ac;
            source.loop = loop;
            source.spatialBlend = 0;
            source.playOnAwake = playOnAwake;
            if (playOnAwake) source.Play();

            return source;
        }
        public static AudioSource Create3DSource(AudioClip ac, string name, AudioMixerGroup mixerGroup, bool loop = false, bool playOnAwake = false)
        {
            Transform cam = AudioManager.Instance.transform;

            var source = cam
                .gameObject
                .CreateDefaultSubObject<AudioSource>("SOURCE-> " + name);

            source.outputAudioMixerGroup = mixerGroup;
            source.clip = ac;
            source.loop = loop;
            source.spatialBlend = 0;
            source.playOnAwake = playOnAwake;
            //source.maxDistance = 35;
            //source.minDistance = 5;
            

            if (playOnAwake) source.Play();

            return source;
        }

        public static void PlayIfNotPlaying(this AudioSource ac)
        {
            if (!ac.isPlaying) ac.Play();
        }

        public static T CreateDefaultSubObject<T>(this GameObject owner, string name) where T : Component
        {
            GameObject go = new GameObject();
            go.name = name;
            T back = go.AddComponent<T>();
            go.transform.SetParent(owner.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = new Vector3(1, 1, 1);
            return back;
        }
    }
}