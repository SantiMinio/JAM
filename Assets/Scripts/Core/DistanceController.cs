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

    float characterDistance;

    private void Start()
    {
        timer = timeToDead;
    }

    private void Update()
    {
        if (timer <= 0) return;

        characterDistance = Vector3.Distance(charOne.position, charTwo.position);

        if(characterDistance > maxDistanceToDead)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                Main.instance.eventManager.TriggerEvent(GameEvents.CharactersSeparate);
                var characters = Main.instance.GetCharacters();
                for (int i = 0; i < characters.Length; i++)
                {
                    characters[i].DeadBySeparate();
                }
            }
        }
        else
        {
            timer = timeToDead;
        }
        UIManager.instance.SetLife(timer / timeToDead);
    }

    public float GetDistanceBetweenCharacters() => characterDistance;
    public float GetMaxDistanceBetweenCharacters() => maxDistanceToDead;
}
