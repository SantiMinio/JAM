using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class PixelShaderCOde : MonoBehaviour
{
  
    public Shader shader;
    Material mat;
    public float Pixel;
    private void Awake()
    {
        mat = new Material(shader);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
    void Update()
    {
        mat.SetFloat("_Pixel", Pixel);
    }
}
