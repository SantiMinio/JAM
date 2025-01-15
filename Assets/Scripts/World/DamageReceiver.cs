using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] List<DamageType> invulnerabilityTypes = new List<DamageType>();

    Dictionary<DamageType, float> damageTypeMultipliers = new Dictionary<DamageType, float>();

    [SerializeField] PhysicsController physics = null;
    [SerializeField] UnityEvent OnInvulnerable = new UnityEvent();
    [SerializeField] UnityEvent OnTakeDamage =  new UnityEvent();
    [SerializeField] UnityEvent OnDeath = new UnityEvent();

    public event Action<Damager> OnTakeDamageEv;
    public event Action<int, int> OnRefreshLifeEv;
    public event Action OnInvulnerableEv;
    public event Action<Damager> OnDeathEv;
    public event Action<Damager> OnHitShield;
    public event Action<Damager> OnDestroyShield;
    public event Func<Damager, bool> CantDamageShield;

    public Func<Damager, bool> IsInvulnerable;

    public LifeSystem lifeSystem;

    bool hasShield;
    int shieldAmmount;

    public void AddShield(int shield)
    {
        shieldAmmount += shield;
        if(shieldAmmount > 0)
            hasShield = true;
        else
        {
            shieldAmmount = 0;
            hasShield = false;
        }
    }

    public void Configure(LifeSystem life)
    {
        lifeSystem = life;
        OnTakeDamageEv += x => OnTakeDamage.Invoke();
        OnDeathEv += x => OnDeath.Invoke();
        OnInvulnerableEv += () => OnInvulnerable.Invoke();

        OnRefreshLifeEv?.Invoke(life.Max, life.Max);
    }

    public void ResetComponent()
    {
        lifeSystem.ResetComponent();
    }

    public void InstaKill()
    {
        lifeSystem.InstaKill();
        OnRefreshLifeEv?.Invoke(lifeSystem.Life, lifeSystem.Max);
        OnDeathEv?.Invoke(new Damager());
    }

    public DamageResult DoDamage(Damager damager)
    {
        var result = new DamageResult();

        if (PauseManager.instance.paused) return result;
        if (!lifeSystem.IsAlive) { Debug.LogWarning("Is Dead"); return result; }


        if((IsInvulnerable != null && IsInvulnerable(damager)) || invulnerabilityTypes.Contains(damager.damageType))
        {
            //  Debug.LogWarning("Invulnerable");
            //Debug.Log( $"<b>{gameObject.name}</b> es invulnerable");
            OnInvulnerableEv();
            return result;
        }

        if (damageTypeMultipliers.ContainsKey(damager.damageType))
            damager.damage = (int)(damager.damage * damageTypeMultipliers[damager.damageType]);


        if (damager.hasKnockback)
        {
            DoKnockback(damager.knockbackModule);
        }


        if (hasShield)
        {
            if (CantDamageShield != null && CantDamageShield(damager)) return result;
            shieldAmmount -= damager.damage;

            if (shieldAmmount <= 0)
            {
                shieldAmmount = 0;
                hasShield = false;
                OnDestroyShield?.Invoke(damager);
            }
            result.damage = damager.damage;
            OnHitShield?.Invoke(damager);
            return result;
        }

        var lastLife = lifeSystem.Life;
        var isAlive = lifeSystem.Hit(damager.damage);
        result.damage = lastLife - lifeSystem.Life;
        Debug.Log(lifeSystem.Life);

        if (!isAlive)
        {

            OnRefreshLifeEv?.Invoke(0, lifeSystem.Max);
            OnDeathEv?.Invoke(damager);
            result.isDeath = true;
            return result;
        }
        OnRefreshLifeEv?.Invoke(lifeSystem.Life, lifeSystem.Max);
        OnTakeDamageEv(damager);
        return result;
    }

    public void DoKnockback(KnockBackModule module)
    {
        Vector3 dir = module.knockbackDir;
        dir.y = 0;
        dir = dir.normalized;
        if(physics) physics.SetForceVector(module.knockbackApplyMode, dir, module.knockbackForce, module.knockbackForceTime, module.knockbackCurve);
    }

    public void Heal(int heal)
    {
        lifeSystem.Heal(heal);
        OnRefreshLifeEv?.Invoke(lifeSystem.Life, lifeSystem.Max);
    }

    public DamageResult DoDamage(int damageAmmount)
    {
        var result = new DamageResult();

        if (!lifeSystem.IsAlive) { Debug.LogWarning("Is Dead"); return result; }

        if (hasShield)
        {
            shieldAmmount -= damageAmmount;
            var dmg = new Damager();
            dmg.damage = damageAmmount;

            if (shieldAmmount <= 0)
            {
                shieldAmmount = 0;
                hasShield = false;
                OnDestroyShield?.Invoke(dmg);
            }
            result.damage = damageAmmount;
            OnHitShield?.Invoke(dmg);
            return result;
        }

        var lastLife = lifeSystem.Life;
        var isAlive = lifeSystem.Hit(damageAmmount);
        result.damage = lastLife - lifeSystem.Life;
        if (!isAlive)
        {

            OnRefreshLifeEv?.Invoke(0, lifeSystem.Max);
            result.isDeath = true;
            return result;
        }
        OnRefreshLifeEv?.Invoke(lifeSystem.Life, lifeSystem.Max);
        return result;
    }

    public void AddMultiplier(DamageType dmgType, float multiplier)
    {
        if (!damageTypeMultipliers.ContainsKey(dmgType))
        {
            damageTypeMultipliers.Add(dmgType, 1);
        }

        damageTypeMultipliers[dmgType] += multiplier;
    }

}
