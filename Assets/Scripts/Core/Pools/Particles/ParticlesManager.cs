﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticlesManager : MonoBehaviour
{
    public static ParticlesManager Instance { get; private set; }

    private Dictionary<string, PoolParticle> particleRegistry = new Dictionary<string, PoolParticle>();

    Dictionary<ParticleSystem, Action> particles = new Dictionary<ParticleSystem, Action>();
    Action ParticlesUpdater = delegate { };

    bool pause;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (!pause) ParticlesUpdater();
    }

    public ParticleSystem PlayParticle(string particleName, Vector3 spawnPos, Transform trackingTransform = null)
    {
        if (particleRegistry.ContainsKey(particleName))
        {
            var particlePool = particleRegistry[particleName];
            particlePool.soundPoolPlaying = true;
            ParticleSystem aS = particlePool.Get();
            aS.transform.position = spawnPos;
            aS.name = particleName;
            if (trackingTransform != null) aS.transform.SetParent(trackingTransform);
            aS.Play();
            if (!aS.main.loop)
            {
                float timer = 0;
                float duration = ObtainDuration(aS);

                Action temp = delegate { };
                Action temp2 = () =>
                {
                    timer += Time.deltaTime;

                    if (timer >= duration)
                    {
                        particles.Remove(aS);
                        particleRegistry[particleName].ReturnParticle(aS);
                        ParticlesUpdater -= temp;
                    }
                };
                temp += temp2;
                ParticlesUpdater += temp;
                if(!particles.ContainsKey(aS))
                    particles.Add(aS, temp);
            }

            return aS;
        }
        else
        {
            Debug.LogWarning("No tenes ese sonido en el pool");
            return null;
        }
    }

    public void PauseParticles(bool pauseOnlyLoop)
    {
        pause = true;
        foreach (var item in particleRegistry)
            item.Value.PauseParticles(pauseOnlyLoop);
    }

    public void ResumeParticles()
    {
        pause = false;
        foreach (var item in particleRegistry)
            item.Value.ResumeParticles();
    }

    public ParticleSystem PlayParticle(string particleName, Vector3 spawnPos, Action callbackEnd, Transform trackingTransform = null)
    {
            //Debug.Log(particleName);
        if (particleRegistry.ContainsKey(particleName))
        {
            var particlePool = particleRegistry[particleName];
            particlePool.soundPoolPlaying = true;
            ParticleSystem aS = particlePool.Get();
            aS.transform.position = spawnPos;
            if (trackingTransform != null) aS.transform.SetParent(trackingTransform);
            aS.name = particleName;
            aS.Play();

            if (!aS.main.loop)
            {
                float timer = 0;
                float duration = particlePool.duration;

                Action temp = delegate { };
                Action temp2 = () =>
                {
                    timer += Time.deltaTime;

                    if (timer >= duration)
                    {
                        particleRegistry[particleName].ReturnParticle(aS);
                        particles.Remove(aS);
                        callbackEnd?.Invoke();
                        ParticlesUpdater -= temp;
                    }
                };
                temp += temp2;
                ParticlesUpdater += temp;
                particles.Add(aS, temp);

                //StartCoroutine(ReturnSoundToPool(aS, particleName));
            }

            return aS;
        }
        else
        {
            Debug.LogWarning("No tenes esa particula en en pool");
            return null;
        }
    }

    public void StopParticle(string particleName, ParticleSystem particle)
    {
        if (particleRegistry.ContainsKey(particleName))
        {
            var particlePool = particleRegistry[particleName];
            particlePool.ReturnParticle(particle);
            if (particles.ContainsKey(particle))
            {
                ParticlesUpdater -= particles[particle];

                particles.Remove(particle);
            }
        }
        else
        {
            Debug.LogWarning("No tenes esa particula en en pool");
        }
    }

    public void StopAllParticles(string particleName = null)
    {
        if (particleName == null)
        {
            foreach (var item in particleRegistry)
            {
                item.Value.StopAllParticles();
            }
            return;
        }

        if (particleRegistry.ContainsKey(particleName))
        {
            particleRegistry[particleName].StopAllParticles();
        }
        else
        {
            Debug.LogWarning("No tenes esa particula en en pool");
        }
    }

    public PoolParticle GetParticlePool(string particleName, ParticleSystem particle = null, int prewarmAmount = 2)
    {
        if (particleRegistry.ContainsKey(particleName)) return particleRegistry[particleName];
        else if (particle != null) return CreateNewParticlePool(particle, particleName, prewarmAmount);
        else return null;
    }

    public void DeleteParticlePool(string particleName)
    {
        if (particleRegistry.ContainsKey(particleName))
        {
            Destroy(particleRegistry[particleName].gameObject);
            particleRegistry.Remove(particleName);
        }
    }

    #region Internal
    private PoolParticle CreateNewParticlePool(ParticleSystem particle, string particleName, int prewarmAmount = 2)
    {
        var particlePool = new GameObject($"{particleName} soundPool").AddComponent<PoolParticle>();
        particlePool.transform.SetParent(transform);
        particlePool.Configure(particle, ObtainDuration(particle));
        particlePool.Initialize(prewarmAmount);
        particleRegistry.Add(particleName, particlePool);
        return particlePool;
    }

    private IEnumerator ReturnSoundToPool(ParticleSystem aS, string sT, Action callbackOnEnd = default)
    {
        while (aS.isPlaying || aS.isPaused) yield return new WaitForSeconds(0.1f);

        if (aS.gameObject.activeSelf)
        {
            particleRegistry[sT].ReturnParticle(aS);
            callbackOnEnd?.Invoke();
        }
    }

    float ObtainDuration(ParticleSystem pS)
    {
        float higher = pS.main.duration;

        var childrens = pS.GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < childrens.Length; i++)
        {
            if (higher < childrens[i].main.duration) higher = childrens[i].main.duration;
        }

        return higher;
    }
    #endregion
}
