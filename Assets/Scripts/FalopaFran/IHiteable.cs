using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHiteable
{
    bool ImInvulnerable();
    bool ImDead();
    bool GetHit(Vector3 dir);

    void SetInvulnerability(bool value);
}
