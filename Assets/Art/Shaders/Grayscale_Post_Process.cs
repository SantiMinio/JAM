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
    [Range (0, 1)]
    public float masOcuro;
    [Range(0, 1)]
    public float fade_in_out;
    public float pixeles;
    public float scale;
    public float speed;

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
        mat.SetFloat("_Pixeles", pixeles);
        mat.SetFloat("_ScaleNoise", scale);
        mat.SetFloat("_Effect", fade_in_out);
        mat.SetFloat("_Speed", speed);
    }
}
