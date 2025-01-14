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
        Collider[] targetsInViewRadious = Physics.OverlapSphere(transform.position, attackRadious).Where(x => x.GetComponent<Hiteable>() != null).ToArray();
        Vector3 dir = Main.instance.GetHusband().currentDir;

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

        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 hitPos = Vector3.zero;

            RaycastHit hit;
            if (Physics.Raycast(Main.instance.GetHusband().transform.position, dir, out hit, attackRadious))
                hitPos = hit.point;

            Hiteable hiteable = targets[i].GetComponent<Hiteable>();
            
            if (hiteable != null) hiteable.GetHit(hitPos);
        }

        slashAnim.transform.localPosition = new Vector3(-dir.x / 2, 0, -dir.z / 2);
        slashAnim.transform.right = dir;
        slashAnim.transform.localEulerAngles = new Vector3(90, slashAnim.transform.localEulerAngles.y, slashAnim.transform.localEulerAngles.z);
    }
}
