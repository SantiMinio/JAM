using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiteable : MonoBehaviour, IHiteable
{
    [SerializeField] private List<DamageType> inmunity;
    
    [SerializeField] private int currentLife, maxLife;
    private bool _imDead = false;
    [SerializeField] private bool canIDie = false;

    [SerializeField] private float timeShaking;

    [SerializeField] private MoveShake _moveShake;

    [SerializeField] private bool _imInvulnerable;
    [SerializeField] string hitableDestroySound = "Hitable_BreakWall";
    [SerializeField] string hitableGetHitSound = "Hitable_GetHit";
    [SerializeField] ParticleSystem rockParticle = null;
    [SerializeField] ParticleSystem destroyedRock = null;

    public event Action onHit;
    public event Action onDead;
    public Func<Vector3, bool> Block = delegate { return false; } ;
    
    public enum DamageType
    {
        laser
    }
    
    public virtual void Start()
    {
        _moveShake = GetComponent<MoveShake>();

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

        SoundFX.PlaySound(hitableGetHitSound);
        
        StopAllCoroutines();
        StartCoroutine(ShakeFeedback());

        if (rockParticle != null)
        {
            rockParticle.transform.position = dir;
            rockParticle.Play();
        }
        
        currentLife--;
        
        if (currentLife <= 0)
        {
            currentLife = 0;
            if (canIDie)
            {
                Dead();
                SoundFX.PlaySound(hitableDestroySound);
            }
            
        }

        onHit?.Invoke();
        
        return true;
    }
    
    public virtual bool GetHit(Vector3 dir,  DamageType type)
    {
        if (Block(dir)) return false;
        if (_imInvulnerable) return false;

        if (inmunity.Contains(type)) return false;
        
        
        
        StopAllCoroutines();
        //if (currentLife <= 0) return false;
        
        StartCoroutine(ShakeFeedback());

        
        
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

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetInvulnerability(bool value)
    {
        _imInvulnerable = value;
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

    IEnumerator ShakeFeedback()
    {
        float time = 0;
        do
        {
            time += Time.deltaTime;
            _moveShake.OnShow();
            yield return new WaitForEndOfFrame();
        } while (time < timeShaking);

        if (_imDead)
        {
            gameObject.SetActive(false);
            destroyedRock.Play();
        }
            
        
    }
}