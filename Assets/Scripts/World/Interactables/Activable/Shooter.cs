using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : ActivableObject
{

    [SerializeField] private float shootIntervalTime;
    
    [SerializeField] private Bullet bullet_pf;
    [SerializeField] private Transform shootOrigin;
    
    
    private void Start()
    {
        PauseManager.instance.AddToPause(this);
        StartCoroutine(ShootRutine());
    }

    IEnumerator ShootRutine()
    {
        do
        {
            if (paused)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            yield return new WaitForSeconds(shootIntervalTime);
            Shoot();    
        } while (!isActive);
        
    }

    private void Shoot()
    {
        Bullet newBullet = Instantiate<Bullet>(bullet_pf, shootOrigin.position, shootOrigin.rotation);
    }

    protected override void On()
    {
        StopAllCoroutines();
        open = true;
    }
    
    protected override void Off()
    {
        StartCoroutine(ShootRutine());
        _count = 0;
        open = false;
    }
}
