using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretAim : ActivableBase
{
    [SerializeField] private Transform rotatePart;
    public float smoothTime = 0.3F;
    public Vector3 velocity = Vector3.zero;

    [SerializeField] private bool followTarget;

    [SerializeField] TargetDetector detector;

    private Lightbeam _lightbeam;

    private void Start()
    {
        _lightbeam = GetComponent<Lightbeam>();
        detector.OnGetNewTarget += FollowNewTarget;
        detector.OnLostTarget += LostTarget;
        detector.TurnOn();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        detector.OnUpdate();

        if (followTarget)
        {
            Follow(detector.CurrentTarget);
        }
    }

    void FollowNewTarget(Transform newTarget)
    {
        Debug.Log("consigue nuevo target?");
        if (_lightbeam.anticipation || _lightbeam.inLightbeam) return;

        _lightbeam.StartAnticipation();
    }

    void LostTarget()
    {
        _lightbeam.StopLightbeam();
    }

    private void Follow(Transform closestChar)
    {
        if (closestChar == null) return;

        Vector3 dir = (closestChar.position - rotatePart.position).normalized;
        Vector3 noY_dir = new Vector3(dir.x, 0, dir.z);
        rotatePart.forward = Vector3.Slerp(rotatePart.forward, noY_dir, smoothTime * Time.deltaTime);
    }
}
