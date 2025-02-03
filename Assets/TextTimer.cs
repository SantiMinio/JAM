using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTimer : MonoBehaviour
{
    float currentTime;

    public float maxTime;
    public bool onZone;
    public GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (onZone)
        {

            currentTime += Time.deltaTime;
            if (currentTime >= maxTime)
            {
                tutorial.SetActive(true);
            }

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterBase>())
        {
            onZone = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterBase>())
        {
            onZone =false;

        }
       
    }
}
