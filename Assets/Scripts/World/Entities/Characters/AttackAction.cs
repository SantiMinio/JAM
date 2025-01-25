using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackAction : CharacterAction
{
    [SerializeField] float attackRadious = 4;
    [SerializeField] float viewAngle = 90;
    [SerializeField] Animator anim = null;
    [SerializeField] string swordSlashSound = null;
    [SerializeField] Damager dmg = new Damager() { damage = 10 };
   


    private void Start()
    {
        var shape = actionParticles[0].GetComponent<ParticleSystem>().shape;
        var shape2 = actionParticles[1].GetComponent<ParticleSystem>().shape;
        shape.radius = attackRadious- attackRadious / 2;
        shape2.radius = attackRadious - attackRadious/2;
    }

    protected override void OnEndAction()
    {
        anim.SetBool("Attack2", false);
    }

    protected override void OnKeepAction()
    {
        anim.SetBool("Attack2", true);        
     
    }

    protected override void OnStartAction()
    {
        SoundFX.PlaySound(swordSlashSound);
        anim.SetTrigger("attack");
       
       
        List<Transform> targets = new List<Transform>();
        Collider[] targetsInViewRadious = Physics.OverlapSphere(transform.position, attackRadious, dmg.rivalsMask).Where(x => x.GetComponent<DamageReceiver>() != null).ToArray();
        Vector3 dir = owner.CurrentDir;
        for (int i = 0; i < targetsInViewRadious.Length; i++)
        {
            Transform target = targetsInViewRadious[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(dir, dirToTarget) < viewAngle)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                targets.Add(target);
            }

        }

        dmg.inflictor = owner.transform;
        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 hitPos = Vector3.zero;


            DamageReceiver hiteable = targets[i].GetComponent<DamageReceiver>();
            if (hiteable != null)
            {
                dmg.knockbackModule.knockbackDir = (hiteable.transform.position - owner.transform.position).normalized;
                hiteable.DoDamage(dmg);
            }
        }

        foreach (GameObject slash in actionParticles)
        {
            slash.transform.forward = dir;
            slash.transform.localPosition = new Vector3(-dir.x / 2, 1.3f, -dir.z / 2);
        }

    }
}
