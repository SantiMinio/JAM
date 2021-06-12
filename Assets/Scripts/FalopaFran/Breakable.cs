using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IBreakable
{
    [SerializeField] private int currentLife, maxLife;
    private bool _imDead = false;

    [SerializeField] private float timeShaking;

    private MoveShake _moveShake;
    
    private void Start()
    {
        _moveShake = GetComponent<MoveShake>();
        
        Reset();
    }

    private void Update()
    {
        
    }

    private void Reset()
    {
        currentLife = maxLife;
    }

    public bool ImDead() => _imDead;


    public void GetHit()
    {
        StartCoroutine(ShakeFeedback());
        currentLife--;
        if (currentLife <= 0)
        {
            _imDead = true;
        }
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
