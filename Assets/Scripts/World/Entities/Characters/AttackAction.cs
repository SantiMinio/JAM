using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

public class AttackAction : CharacterAction
{
    [SerializeField] float attackRadious = 4;
    [SerializeField] float viewAngle = 90;
    [SerializeField] Animator anim = null;
    [SerializeField] string swordSlashSound = null;
    [SerializeField] Damager dmg = new Damager() { damage = 10 };
    [SerializeField] TriggerInteract damagerInteract = null;
    [SerializeField] int damage = 10;
    [SerializeField] AnimEvent animEvent = null;

    private void Start()
    {
        var shape = actionParticles[0].GetComponent<ParticleSystem>().shape;
        var shape2 = actionParticles[1].GetComponent<ParticleSystem>().shape;
        shape.radius = attackRadious- attackRadious / 2;
        shape2.radius = attackRadious - attackRadious/2;
        animEvent.Add_Callback("OpenAttack", OpenAttackWindow);
        animEvent.Add_Callback("CloseAttack", CloseAttackWindow);
        damagerInteract.OnColliderEnter += DamageOnCollide;
        damagerInteract.gameObject.SetActive(false);
    }

    protected override void OnEndAction()
    {
    }

    protected override void OnKeepAction()
    {
    }

    protected override void OnStartAction()
    {
        SoundFX.PlaySound(swordSlashSound);
        anim.SetBool("Attack2", true);

        Vector3 dir = owner.CurrentDir;

        foreach (GameObject slash in actionParticles)
        {
            slash.transform.forward = dir;
            slash.transform.localPosition = new Vector3(-dir.x / 2, 1.3f, -dir.z / 2);
        }

    }

    void DamageOnCollide(Collider col)
    {
        var target = col.GetComponent<DamageReceiver>();

        if(!target) return;

        dmg.inflictor = owner.transform;
        dmg.knockbackModule.knockbackDir = (target.transform.position - owner.transform.position).normalized;
        target.DoDamage(dmg);
    }

    void OpenAttackWindow()
    {
        damagerInteract.gameObject.SetActive(true);
    }

    void CloseAttackWindow()
    {
        damagerInteract.gameObject.SetActive(false);

        anim.SetBool("Attack2", false);
    }
}
