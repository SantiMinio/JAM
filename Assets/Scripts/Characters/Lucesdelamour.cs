using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucesdelamour : MonoBehaviour
{  
    
    public Light luzPibe;
    public Light luzPiba;
    public Light LoveLight;
    public float loveRange;
    public float loveMaxRange;
    public float IntensityMultipler;
    public float IntensityMultipler2;
    public float gppmultipler;
    public DistanceController dc;
    private void Awake()
    {
        LoveLight.gameObject.SetActive(false);
        loveMaxRange = dc.GetMaxDistanceBetweenCharacters();
        loveRange = dc.GetDistanceBetweenCharacters();       
    }

    private void Update()
    {
        luzPiba.transform.position = Main.instance.GetWife().transform.position + Vector3.up ;
        luzPibe.transform.position = Main.instance.GetHusband().transform.position + Vector3.up;
        luzPiba.intensity = -dc.GetDistanceBetweenCharacters() * IntensityMultipler + loveMaxRange;
        luzPibe.intensity = -dc.GetDistanceBetweenCharacters() * IntensityMultipler + loveMaxRange;
       
        if (dc.GetDistanceBetweenCharacters() < 2)
        {
            luzPiba.gameObject.SetActive(false);
            luzPibe.gameObject.SetActive(false);
            LoveLight.gameObject.SetActive(true);
            LoveLight.intensity= -dc.GetDistanceBetweenCharacters() * IntensityMultipler2 + loveMaxRange;
        }
        else
        {
            luzPiba.gameObject.SetActive(true);
            luzPibe.gameObject.SetActive(true);
            LoveLight.gameObject.SetActive(false);
        }
        LoveLight.transform.position = Vector3.Lerp(luzPiba.transform.position, luzPibe.transform.position, 0.5f);
    }
}
