using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KissAction : MonoBehaviour
{
    public float kissOffset = 0;
    public float lerpDuration;
    public Transform partner;
    public Transform model;
    public bool canKiss;
    public Animator myAnim;
    public Animator canvasAnim;
    public DistanceController distanceController;
    public bool kissing;

    public void Kiss()
    {
        StartCoroutine(KissCorrutine());
    }
    IEnumerator KissCorrutine()
    {
        Vector3 startPos = partner.transform.position;
        float currentTime = 0;
        myAnim.SetTrigger("kiss");
        while (currentTime < lerpDuration)
        {
            distanceController.Heal();
            partner.transform.position = Vector3.Lerp(startPos, transform.position + model.forward * kissOffset, currentTime / lerpDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        canvasAnim.SetTrigger("kiss");
        distanceController.Heal();
        kissing = true;


    }
    public void kissAnimEnd()
    {
        kissing = false;
    }
}
