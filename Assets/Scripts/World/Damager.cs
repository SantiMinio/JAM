using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Damager
{
    public int damage;
    public bool hasKnockback;
    public DamageType damageType;
    public Transform inflictor;
    public KnockBackModule knockbackModule;
    public LayerMask rivalsMask;
    public string dmgID;
}

[Serializable]
public struct KnockBackModule
{
    public Vector3 knockbackDir;
    public float knockbackForce;
    public float knockbackForceTime;
    public ForceApplyMode knockbackApplyMode;
    public AnimationCurve knockbackCurve;
}

[Serializable]
public struct DamageResult
{
    public bool isDeath;
    public int damage;
}
