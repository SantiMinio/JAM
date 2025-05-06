using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FullscreenEffectController : MonoBehaviour
{
    void Start()
    {
        // Obtener el renderer actual
        var renderer = (UniversalRenderPipeline.asset.scriptableRenderer as ScriptableRenderer);


        // Este cast puede variar según el nombre de la clase interna usada
        /*  var materialField = feature.GetType().GetField("m_Material", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
          if (materialField != null)
          {
              var mat = materialField.GetValue(feature) as Material;

              // Cambiar algún parámetro del shader (ejemplo: "_Intensity")
              if (mat != null)
              {
                  mat.SetFloat("_Intensity", 0.5f); // Cambialo por tu propiedad
              }
          }*/
    }
}