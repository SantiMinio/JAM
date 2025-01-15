using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackAction : CharacterAction
{
    [SerializeField] float attackRadious = 4;
    [SerializeField] float viewAngle = 90;
    [SerializeField] Animator anim = null;
    [SerializeField] Animator slashAnim;
    [SerializeField] string swordSlashSound = null;
    [SerializeField] Damager dmg = new Damager() { damage = 10 };
    

    private void Start()
    {
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
        slashAnim.Play("Slash");
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

        slashAnim.transform.localPosition = new Vector3(-dir.x / 2, 0, -dir.z / 2);
        slashAnim.transform.right = dir;
        slashAnim.transform.localEulerAngles = new Vector3(90, slashAnim.transform.localEulerAngles.y, slashAnim.transform.localEulerAngles.z);
    }
}
