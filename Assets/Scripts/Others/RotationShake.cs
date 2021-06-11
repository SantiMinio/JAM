using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationShake : MonoBehaviour
{
    [SerializeField] Transform torot = null;
    Vector3 startPos;
    [SerializeField] float shakeAmmount = 5;
    [SerializeField] float shakeTime = 0.2f;
    float timer;

    private void Start()
    {
        startPos = torot.localEulerAngles;
    }
    public void OnShow()
    {
        if (timer < shakeTime)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;

        Vector3 _randomPos = startPos + (Random.insideUnitSphere * shakeAmmount);
        torot.localEulerAngles = _randomPos;
    }
    public void OnHide() { if (torot) torot.localEulerAngles = startPos; timer = 0; }
}
