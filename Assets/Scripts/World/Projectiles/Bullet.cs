using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPause
{
    private Rigidbody _rb;

    [SerializeField] private float speed, lifeTime;

    [SerializeField] private LayerMask triggerLayers;

    private float _count;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        PauseManager.instance.AddToPause(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            var character = other.GetComponent<IHiteable>();
            Vector3 dir = (character.GetPosition() - transform.position).normalized;

            character.GetHit(dir);
        }
        
        Destroy(gameObject);
        
        
    }

    private void Update()
    {
        if (paused) return;
        _count += Time.deltaTime;

        if (_count >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (paused) return;
        _rb.velocity = transform.forward * speed;
    }

    bool paused;

    public void Pause()
    {
        paused = true;
        _rb.velocity = Vector3.zero;
    }

    public void Resume()
    {
        paused = false;
    }
}
