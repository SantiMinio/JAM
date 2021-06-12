using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Grayscale_Post_Process : MonoBehaviour
{
    public Shader shader;
    Material mat;
    [Range (0, 1)]
    public float grayscale;
    [Range (1, 3)]
    public float masOcuro;

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
        mat.SetFloat("_Grayscale", grayscale);
        mat.SetFloat("_MasOcuro", masOcuro);
    }
}
