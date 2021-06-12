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

    void Dead()
    {
        _imDead = true;
        onDead?.Invoke();
        Main.instance.eventManager.TriggerEvent(GameEvents.OneCharDie);
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
