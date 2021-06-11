using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedClass
{
    [SerializeField] float checkpointX = 0;
    [SerializeField] float checkpointY = 0;
    [SerializeField] float checkpointZ = 0;

    public Vector3 GetCheckPoint()
    {
        return new Vector3(checkpointX, checkpointY, checkpointZ);
    }

    public void SaveCheckPoint(Vector3 check)
    {
        checkpointX = check.x;
        checkpointY = check.y;
        checkpointZ = check.z;
    }
}
