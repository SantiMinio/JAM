using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceController : MonoBehaviour, IPause
{
    [SerializeField] float maxDistanceToDead = 10;
    [SerializeField] float timeToDead = 3;
    [SerializeField]
    float timer;

    [SerializeField] Transform charOne = null;
    [SerializeField] Transform charTwo = null;
    [SerializeField] Material grayscalePass = null;
    //old
    //[SerializeField] Grayscale_Post_Process gpp = null;

    float characterDistance;

    private void Start()
    {
        grayscalePass.SetFloat("_Intensity", 0);
        timer = timeToDead;
        PauseManager.instance.AddToPause(this);
    }

    private void Update()
    {
        if (paused || timer <= 0) return;

        characterDistance = Vector3.Distance(charOne.position, charTwo.position);

        if(characterDistance > maxDistanceToDead)
        {
            timer -= Time.deltaTime;
            float t = 1f - (timer / timeToDead);
      //      grayscalePass.SetFloat("_Intensity", t);

            if (timer <= 0)
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
            if (timer >= timeToDead) return;
            timer += Time.deltaTime;
            if(timer >= timeToDead)
                timer = timeToDead;
        }
        var value = Mathf.Lerp(1,0,timer/timeToDead);
        grayscalePass.SetFloat("_Intensity", value);
        UIManager.instance.SetLife(timer / timeToDead);
    }

    public float GetDistanceBetweenCharacters() => characterDistance;
    public float GetMaxDistanceBetweenCharacters() => maxDistanceToDead;

    bool paused;

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }
}
