using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiteable : MonoBehaviour, IHiteable
{
    [SerializeField] private int currentLife, maxLife;
    private bool _imDead = false;

    [SerializeField] private float timeShaking;

    private MoveShake _moveShake;

    private bool _imInvulnerable;
    
    private void Start()
    {
        _moveShake = GetComponent<MoveShake>();
        
        Reset();
    }
    private void Reset()
    {
        currentLife = maxLife;
    }
    public bool ImInvulnerable() => _imInvulnerable;
    
    public bool ImDead() => _imDead;
    
    public void GetHit()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeFeedback());
        currentLife--;
        if (currentLife <= 0)
        {
            _imDead = true;
        }
    }
    
    public bool GetHit(Vector3 dir)
    {
        if (_imInvulnerable) return false;
        
        StopAllCoroutines();
        StartCoroutine(ShakeFeedback());
        currentLife--;
        if (currentLife <= 0)
        {
            _imDead = true;
        }

        return true;
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
