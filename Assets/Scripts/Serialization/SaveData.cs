using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData saveData { get; private set; }
    public Vector3 checkpointPosition;

    private void Awake()
    {
        transform.SetParent(null);
        if (saveData == null)
        {
            saveData = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
