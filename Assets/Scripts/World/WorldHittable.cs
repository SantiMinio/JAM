using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldHittable : MonoBehaviour, IHiteable
{
    [SerializeField] private int currentLife, maxLife;
    private bool _imDead = false;
    [SerializeField] private bool canIDie = false;

    [SerializeField] private bool _imInvulnerable;

    [SerializeField] private List<Hiteable.DamageType> inmunity;
    
    public event Action onHit;
    public event Action onDead;
    public Func<Vector3, bool> Block = delegate { return false; };

    public virtual void Start()
    {
        Reset();
    }
    private void Reset()
    {
        currentLife = maxLife;
    }
    public bool ImInvulnerable() => _imInvulnerable;
    public bool CanIDie() => canIDie;
    public bool ImDead() => _imDead;

    public virtual bool GetHit(Vector3 dir)
    {
        if (_imDead) return false;
        if (Block(dir)) return false;
        if (_imInvulnerable) return false;

        currentLife--;

        if (currentLife <= 0)
        {
            currentLife = 0;
            if (canIDie)
            {
                Dead();
            }

        }

        onHit?.Invoke();

        return true;
    }

    public bool GetHit(Vector3 dir, Hiteable.DamageType damageType)
    {
        if (_imDead) return false;
        if (Block(dir)) return false;
        if (_imInvulnerable) return false;

        if (inmunity.Contains(damageType)) return false;
        
        StopAllCoroutines();
        
        currentLife--;
        
        if (currentLife <= 0)
        {
            currentLife = 0;
            if (canIDie)
            {
                Dead();
            }
            
        }

        onHit?.Invoke();
        
        return true;
    }

    void Dead()
    {
        _imDead = true;
        onDead?.Invoke();
    }
    
    public void InstaKill()
    {
        Dead();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetInvulnerability(bool value)
    {
        _imInvulnerable = value;
    }
}
