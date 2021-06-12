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
    [SerializeField] AudioClip slashSound = null;

    private void Start()
    {
        AudioManager.instance.GetSoundPool(slashSound.name, AudioManager.SoundDimesion.TwoD, slashSound);
    }

    protected override void OnEndAction()
    {
    }

    protected override void OnKeepAction()
    {
    }

    protected override void OnStartAction()
    {
        AudioManager.instance.PlaySound(slashSound.name);
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
            Hiteable hiteable = targets[i].GetComponent<Hiteable>();

            Vector3 attackDir = (hiteable.GetPosition() - transform.position).normalized; 
            
            if (hiteable != null) hiteable.GetHit(attackDir);
        }

        slashAnim.transform.localPosition = new Vector3(-dir.x / 2, 0, -dir.z / 2);
        slashAnim.transform.right = dir;
        slashAnim.transform.localEulerAngles = new Vector3(90, slashAnim.transform.localEulerAngles.y, slashAnim.transform.localEulerAngles.z);
    }
}
