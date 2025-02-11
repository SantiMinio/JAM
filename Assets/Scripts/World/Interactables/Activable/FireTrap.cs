using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireTrap : ActivableBase
{
    [SerializeField] private ParticleSystem feedbackParticles;

    [SerializeField] TriggerInteract interactDmg;
    [SerializeField] Damager dmg;

    [SerializeField] float minimumActivatedTime = 2;
    bool stillActivated;

    private void Start()
    {
        interactDmg.OnColliderEnter += HitCloseCharacters;
    }

    protected override void OnActivate()
    {
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

        feedbackParticles.Stop();
        interactDmg.gameObject.SetActive(false);
    }
}
