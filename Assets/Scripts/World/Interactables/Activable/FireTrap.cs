using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class FireTrap : ActivableObject
{
    [SerializeField] private ParticleSystem feedbackParticles;

    [SerializeField] private float timeToActivateTrap, radiusEffect;
    [SerializeField] Damager dmg;
    
    protected override void On()
    {
        open = true;
        StartCoroutine(WaitUntilActivateTrap());
    }

    IEnumerator WaitUntilActivateTrap()
    {
        float _count = 0;
        do
        {
            if (paused)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            _count += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (_count <= timeToActivateTrap);
     
        feedbackParticles.Play();
        HitCloseCharacters();

    }

    private void HitCloseCharacters()
    {
        var charsCloseToGetHit = Main.instance.GetCharacters()
            .Where(x => Vector3.Distance(x.transform.position, transform.position) <= radiusEffect)
            .Select(x => x.GetComponent<DamageReceiver>());

        foreach (var charClose in charsCloseToGetHit)
        {
            Debug.Log("le pego a " + charClose);
            Vector3 dir = (charClose.transform.position - transform.position).normalized;
            dmg.inflictor = transform;
            dmg.knockbackModule.knockbackDir = dir;

            charClose.DoDamage(dmg);
        }
        
        
    }

    protected override void Off()
    {
        _count = 0;
        open = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusEffect);
    }
}
