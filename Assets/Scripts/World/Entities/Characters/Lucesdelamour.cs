using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucesdelamour : MonoBehaviour
{  
    
    
    public float loveRange;
    public float loveMaxRange;
    public float IntensityMultipler;
    public float IntensityMultipler2;
    public float gppmultipler;
    public DistanceController dc;
    private void Awake()
    {      
        loveMaxRange = dc.GetMaxDistanceBetweenCharacters();
        loveRange = dc.GetDistanceBetweenCharacters();       
    }

    private void Update()
    {             
        
    }
}
