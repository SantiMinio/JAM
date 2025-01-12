using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] UIPanelTransition panelTransition = null;

    private void Start()
    {
        panelTransition.OnCloseOver += () => Debug.Log("Close");
        panelTransition.OnOpenOver += () => Debug.Log("Open");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            panelTransition.Open();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            panelTransition.Close();
        }
    }
}
