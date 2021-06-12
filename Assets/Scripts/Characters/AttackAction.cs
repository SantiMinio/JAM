using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackAction : CharacterAction
{
    [SerializeField] float attackRadious = 4;
    [SerializeField] float viewAngle = 90;

    protected override void OnEndAction()
    {
    }

    protected override void OnKeepAction()
    {
    }

    protected override void OnStartAction()
    {
        List<Transform> targets = new List<Transform>();
        Collider[] targetsInViewRadious = Physics.OverlapSphere(transform.position, attackRadious).Where(x => x.GetComponent<Hiteable>() != null).ToArray();

        for (int i = 0; i < targetsInViewRadious.Length; i++)
        {
            Transform target = targetsInViewRadious[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
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
    }
}
