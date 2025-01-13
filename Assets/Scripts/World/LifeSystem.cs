using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeSystem
{
    Action Death = delegate { };
    Action OnTakeDamage = delegate { };
    event Action OnLifeChange = delegate { };
    int life = 0;
    int max = 0;
    bool isAlive = false;
    public bool IsAlive => isAlive;
    public int Life => life;
    public int Max => max;
    public float Lerp => (float)life / (float)max;
    public float LifePercent
    {
        get 
        {
            return Lerp * 100;
        }
    }

    public void SubscribeLifeChange(Action lifeChange)
    {
        OnLifeChange += lifeChange;
    }
    public void RemoveLifeChange(Action lifeChange)
    {
        OnLifeChange -= lifeChange;
    }
    public void RetargetSubscritionToDeath(Action deathCallback)
    {
        Death = deathCallback;
    }

    public LifeSystem(int max)
    {
        this.life = max;
        this.max = max;
        isAlive = max != 0;
        OnLifeChange.Invoke();
    }
    public LifeSystem(int max, Action _Death, Action _TakeDamage)
    {
        this.life = max;
        this.max = max;
        Death = _Death;
        isAlive = max != 0;
        OnTakeDamage = _TakeDamage;
        OnLifeChange.Invoke();
    }

    public bool Heal(int heal_value = 1)
    {
        if (isAlive && life < max)
        {
            life += heal_value;
            if (life > max) life = max;
            OnLifeChange.Invoke();
            return true;
        }

        return false;
    }
    public bool InstaHeal()
    {
        if (isAlive && life < max)
        {
            life = max;
            OnLifeChange.Invoke();
            return true;

        }

        return false;
    }
    public void ResetComponent()
    {
        life = max;
        OnLifeChange.Invoke();
        isAlive = true;
    }
    public bool Hit(int damage = 1)
    {
        if (isAlive)
        {
            life -= damage;
            OnTakeDamage.Invoke();
            OnLifeChange.Invoke();
            if (life <= 0)
            {
                life = 0;
                Death.Invoke();
                isAlive = false;
                return false;
            }
            return true;
        }
        return false;     
    }
    public bool InstaKill()
    {
        if (isAlive)
        {
            life = 0;
            Death.Invoke();
            isAlive = false;
            OnLifeChange.Invoke();
            return true;
        }
        return false;
    }
    public void ResurrectToMax()
    {
        if (isAlive) return;
        life = max;
        isAlive = true;
        OnLifeChange.Invoke();
    }
    public void ResurrectToValue(int value = 20)
    {
        if (isAlive) return;
        life = value;
        if (value > max) life = max;
        isAlive = true;
        OnLifeChange.Invoke();
    }
    public void ResurrectToPercentual(int percent = 20)
    {
        if (isAlive) return;
        float aux = (float)percent * 100 / max;
        life = (int)aux;
        isAlive = true;
        OnLifeChange.Invoke();
    }
    public void Add(int quant)
    {
        if (!isAlive) return;
        max += quant;
        OnLifeChange.Invoke();
    }
    public void Reconfigure(int val, int max = -1)
    {
        life = val;
        if (max != -1) this.max = max;
        OnLifeChange.Invoke();
    }
}
