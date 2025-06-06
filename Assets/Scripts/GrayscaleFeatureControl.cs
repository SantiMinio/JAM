using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayscaleFeatureControl : MonoBehaviour
{
    public Material grayscaleMat;
    float progressValue = 0f;
    public bool isAnimating = false;
    public float duration = 2f;

    // Update is called once per frame
    void Update()
    {

     
    }

    private IEnumerator TriggerOn()
    {
        isAnimating = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float currentValue = Mathf.Lerp(0, 1, t);
            grayscaleMat.SetFloat("_Intensity", currentValue);

            elapsed += Time.deltaTime;
            yield return null; // Espera al siguiente frame
        }

        // Asegura que llegue al valor final
        grayscaleMat.SetFloat("_Intensity", 1);
        isAnimating = false;
    }

    private IEnumerator TriggerOf()
    {
        isAnimating = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float currentValue = Mathf.Lerp(1, 0, t);
            grayscaleMat.SetFloat("_Intensity", currentValue);
            elapsed += Time.deltaTime; ;
            yield return null;
        }
           

    }


}
