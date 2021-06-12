using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiteable : MonoBehaviour, IHiteable
{
    [SerializeField] private int currentLife, maxLife;
    private bool _imDead = false;
    [SerializeField] private bool canIDie = false;

    [SerializeField] private float timeShaking;

    [SerializeField] private MoveShake _moveShake;

    [SerializeField] private bool _imInvulnerable;

    public event Action onHit;
    
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
        if (_imInvulnerable) return false;
        
        StopAllCoroutines();
        StartCoroutine(ShakeFeedback());

        
        
        currentLife--;
        
        if (currentLife <= 0)
        {
            currentLife = 0;
            if (canIDie)
            {
                _imDead = true;
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

    IEnumerator ShakeFeedback()
    {
        float time = 0;
        do
        {
            time += Time.deltaTime;
            _moveShake.OnShow();
            yield return new WaitForEndOfFrame();
        } while (time < timeShaking);
        
        if(_imDead) gameObject.SetActive(false);
        
    }
}
