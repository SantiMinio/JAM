using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhysicsController : MonoBehaviour
{

    [SerializeField] Rigidbody rb = null;
    [SerializeField] float gravity = -5;

    float firstGravity;


    public Vector3 Velocity => rb.velocity;
    Vector3 movementVector;

    Vector3 forceDir;
    Vector3 forceResult;

    bool applyingForce;
    float timeApplyingForce;
    float forceMultiplier;
    float forceTimer;
    AnimationCurve forceCurve;
    Action OnEndForce;

    [SerializeField] GroundSensor groundSensor;


    private void Awake()
    {
        firstGravity = gravity;
    }

    public void SetMovementVector(Vector3 _movementVector)
    {
           
        movementVector = _movementVector;
    }
    bool paused;
    Vector3 velocity;
    public void Pause()
    {
        paused = true;
        velocity = rb.velocity;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void Resume()
    {
        if (!paused) return;
        paused = false;
        rb.velocity = velocity;
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    public void StopPhysics()
    {
        movementVector = Vector3.zero;
        forceResult = Vector3.zero;
        applyingForce = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void ReactivatePhysiscs(RigidbodyConstraints constrains)
    {
        rb.constraints = constrains;
    }

    public void SetForceVector(ForceApplyMode applyMode, Vector3 _forceDir, float _forceMultiplier, float forceTime, AnimationCurve curve = null, Action _OnEndForce = null)
    {
        if (applyMode == ForceApplyMode.Passive && applyingForce) return;

        forceTimer = 0;

        if (applyMode == ForceApplyMode.Cancel || (applyMode == ForceApplyMode.Additive && !applyingForce))
        {
            OnEndForce?.Invoke();
            OnEndForce = null;
            forceDir = _forceDir;
            forceMultiplier = _forceMultiplier;
            timeApplyingForce = forceTime;
            forceCurve = curve;
        }
        else if(applyMode == ForceApplyMode.Additive)
        {
            forceDir += _forceDir;
            forceDir.Normalize();
            forceMultiplier += _forceMultiplier;
            timeApplyingForce = forceTime;
            forceCurve = curve;
        }

        applyingForce = true;


        OnEndForce += _OnEndForce;
    }

    public void SetJump(float jumpForce, ForceMode forceMode)
    {
        rb.AddForce(Vector3.up * jumpForce, forceMode);   
    }

    public void SetGravity(float _gravity)
    {
        gravity = _gravity;
    }

    public void ReturnToFirstGravity()
    {
        gravity = firstGravity;
    }

    private void FixedUpdate()
    {
        if (paused) return;

        if (applyingForce)
        {
            forceTimer += Time.fixedDeltaTime;

            if(forceTimer >= timeApplyingForce)
            {
                applyingForce = false;
                forceTimer = 0;
                forceResult = Vector3.zero;
                OnEndForce?.Invoke();
                OnEndForce = null;
                return;
            }
            forceResult = forceDir * forceMultiplier;

            if(forceCurve != null)
            {
                forceResult *= forceCurve.Evaluate(forceTimer / timeApplyingForce);
            }

        }

       
        if(groundSensor != null)
        {
            if (!applyingForce)
            {
                rb.velocity = movementVector + forceResult;
                if (!groundSensor.IsGrounding) rb.velocity = new Vector3(rb.velocity.x, gravity, rb.velocity.z);
            }
            else
            {
                rb.velocity = movementVector + forceResult;
            }
        }
        else
        {
            rb.velocity = movementVector + forceResult + new Vector3(0, rb.velocity.y, 0);
        }
    }
}




public enum ForceApplyMode
{
    Additive,
    Cancel,
    Passive
}