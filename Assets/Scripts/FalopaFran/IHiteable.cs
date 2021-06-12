using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHiteable
{
    bool ImInvulnerable();
    bool CanIDie();
    bool ImDead();
    bool GetHit(Vector3 dir);
    bool GetHit(Vector3 dir, Hiteable.DamageType damageType);

    Vector3 GetPosition();
    void SetInvulnerability(bool value);

    void InstaKill();
}
