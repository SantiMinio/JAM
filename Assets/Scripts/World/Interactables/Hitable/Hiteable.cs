using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hiteable : Entity
{
    [SerializeField] private float timeShaking;

    [SerializeField] private MoveShake _moveShake;

    [SerializeField] string hitableDestroySound = "Hitable_BreakWall";
    [SerializeField] string hitableGetHitSound = "Hitable_GetHit";
    [SerializeField] ParticleSystem rockParticle = null;
    [SerializeField] ParticleSystem destroyedRock = null;

    public event Action onHit;
    public event Action onDead;

    [SerializeField] bool invencible = false;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    protected override void OnInvulerable()
    {
        base.OnInvulerable();
        onHit?.Invoke();
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

        if (IsDead)
        {
            gameObject.SetActive(false);
            if (destroyedRock != null)
            {
                destroyedRock.transform.parent = null;
                destroyedRock.Play();
            }
        }
    }

    protected override void OnTurnOff()
    {
    }

    protected override void OnTurnOn()
    {
    }

    protected override void OnInitialize()
    {
        _moveShake = GetComponent<MoveShake>();
    }

    protected override void OnUpdate()
    {
    }

    protected override void TakeDamage(Damager dmg)
    {
        SoundFX.PlaySound(hitableGetHitSound);

        StopAllCoroutines();
        StartCoroutine(ShakeFeedback());

        if (rockParticle != null)
        {
            Vector3 position = Vector3.zero;
            if (Physics.Raycast(dmg.inflictor.position, dmg.knockbackModule.knockbackDir, out RaycastHit hit, 1000))
            {
                position = hit.point;
            }

            rockParticle.transform.position = position;
            rockParticle.Play();
        }

        onHit?.Invoke();

    }

    protected override bool OnIsInvulnerable(Damager dmg)
    {
        return invencible;
    }

    protected override void OnDeath()
    {
        SoundFX.PlaySound(hitableDestroySound);
        StartCoroutine(ShakeFeedback());
        onDead?.Invoke();
    }
}
