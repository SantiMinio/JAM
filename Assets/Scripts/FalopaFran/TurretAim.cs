using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretAim : ActivableBase
{
    private bool isOn = true;

    [SerializeField] private Transform rotatePart;
    public float smoothTime = 0.3F;
    public Vector3 velocity = Vector3.zero;

    [SerializeField] private float radius;

    [SerializeField] private bool followTarget;
    
    private Lightbeam _lightbeam;

    private void Start()
    {
        _lightbeam = GetComponent<Lightbeam>();
    }

    private void Update()
    {
        if(followTarget)
         FollowClosestCharacter();
        
        
        if (!isActive)
        {
            SetActive(true);
            return;
        }

        if (isOn)
            SetActive(false);

    }

    private void FollowClosestCharacter()
    {
        var closestChar = Physics
            .OverlapSphere(transform.position, radius)
            .Select(x=> x.GetComponent<Rigidbody>())
            .Where(x => x != null && x.gameObject.tag == "rayTarget")
            .OrderBy(x => Vector3.Distance(x.transform.position, transform.position));
        
        if(!closestChar.Any()) return;
        
        if (Vector3.Distance(closestChar.First().transform.position, transform.position) <= radius)
        {
            Follow(closestChar.First().transform);
        }
        
        
    }

    private void Follow(Transform closestChar)
    {
        Vector3 dir = (closestChar.position - rotatePart.position).normalized;
        Vector3 noY_dir = new Vector3(dir.x, 0, dir.z);
        rotatePart.forward = Vector3.SmoothDamp(rotatePart.forward, noY_dir, ref velocity, smoothTime);
    }

    private void SetActive(bool b)
    {
        isOn = b;
        _lightbeam.SetActive(b);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
