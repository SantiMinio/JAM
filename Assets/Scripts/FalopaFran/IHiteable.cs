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

    Vector3 GetPosition();
    void SetInvulnerability(bool value);
}
