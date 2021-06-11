using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IBreakable
{
    [SerializeField] private int currentLife, maxLife;
    private bool _imDead = false;

    private MoveShake _moveShake;
    
    private void Start()
    {
        _moveShake = GetComponent<MoveShake>();
        
        Reset();
    }

    private void Reset()
    {
        currentLife = maxLife;
    }

    public bool ImDead() => _imDead;


    public void GetHit()
    {
        
        currentLife--;
        if (currentLife <= 0)
        {
            _imDead = true;
            gameObject.SetActive(false);
        }
    }
}
