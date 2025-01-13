using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3();
    [SerializeField] float radius = 0.1f;
    [SerializeField] LayerMask layer;

    bool isGrounding;
    public bool IsGrounding { get { return isGrounding; } }

    public event Action<bool> OnGroundChange;

    private void Update()
    {
        bool tempIsGrounding = IsGrounding;
        isGrounding = Physics.CheckSphere(this.transform.position + offset, radius, layer);

        if (isGrounding != tempIsGrounding)
            OnGroundChange?.Invoke(isGrounding);
    }


    private const float CABEZA_MAGO = 6.5f;
    private const float RADIUS_GIZMOS_DEB = 1f;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position + offset, radius);
        Gizmos.color = isGrounding ? Color.green : Color.red;
        Gizmos.DrawSphere(this.transform.position + new Vector3(0, CABEZA_MAGO, 0), RADIUS_GIZMOS_DEB);
    }
}
