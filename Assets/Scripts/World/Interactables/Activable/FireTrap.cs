using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireTrap : ActivableBase
{
    [SerializeField] private ParticleSystem feedbackParticles;
    [SerializeField] private ParticleSystem sparksParticles;

    ParticleSystem currentFire;
    ParticleSystem currentSparks;

    [SerializeField] TriggerInteract interactDmg;
    [SerializeField] Damager dmg;

    [SerializeField] float minimumActivatedTime = 2;
    bool stillActivated;

    private void Start()
    {
        interactDmg.OnColliderEnter += HitCloseCharacters;
        ParticlesManager.Instance.GetParticlePool(feedbackParticles.name, feedbackParticles, 10);
        ParticlesManager.Instance.GetParticlePool(sparksParticles.name, sparksParticles, 10);
    }

    protected override void OnActivate()
    {
        if (currentSparks != null)
        {
            ParticlesManager.Instance.StopParticle(sparksParticles.name, currentSparks);
            currentSparks = null;
        }
        currentFire = ParticlesManager.Instance.PlayParticle(feedbackParticles.name, transform.position, transform);
        feedbackParticles.Play();
        interactDmg.gameObject.SetActive(true);
        stillActivated = true;
        TimerManager.Instance.AddTimer(minimumActivatedTime, () => { }, Check);
    }

    private void HitCloseCharacters(Collider col)
    {
        var charsCloseToGetHit = col.GetComponent<DamageReceiver>();

        if (charsCloseToGetHit == null) return;
        Debug.Log("le pego a " + charsCloseToGetHit.name);
        Vector3 dir = (charsCloseToGetHit.transform.position - transform.position).normalized;
        dmg.inflictor = transform;
        dmg.knockbackModule.knockbackDir = dir;

        charsCloseToGetHit.DoDamage(dmg);
    }

    void Check()
    {
        stillActivated = false;
        if (!isActive)
            OnDeactivate();
    }

    protected override void OnDeactivate()
    {
        if (stillActivated) return;

        if(currentFire != null)
        {
            ParticlesManager.Instance.StopParticle(feedbackParticles.name, currentFire);
            currentFire = null;
        }
        feedbackParticles.Stop();
        interactDmg.gameObject.SetActive(false);
    }

    public override void Anticipation()
    {
        base.Anticipation();
        currentSparks = ParticlesManager.Instance.PlayParticle(sparksParticles.name, transform.position, transform);
    }
}
