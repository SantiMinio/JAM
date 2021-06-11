using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceController : MonoBehaviour
{
    [SerializeField] float maxDistanceToDead = 10;
    [SerializeField] float timeToDead = 3;
    [SerializeField]
    float timer;

    [SerializeField] Transform charOne = null;
    [SerializeField] Transform charTwo = null;



    private void Update()
    {
        if (timer >= timeToDead) return;

        float distance = Vector3.Distance(charOne.position, charTwo.position);

        if(distance > maxDistanceToDead)
        {
            timer += Time.deltaTime;

            if(timer >= timeToDead)
            {
                Main.instance.eventManager.TriggerEvent(GameEvents.CharactersSeparate);
            }
        }
        else
        {
            timer = 0;
        }
    }
}
