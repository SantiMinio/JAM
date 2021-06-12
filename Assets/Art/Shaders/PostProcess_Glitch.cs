using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PostProcess_Glitch : MonoBehaviour
{
    public Shader shader;
    public Texture pencilStroke;
    Material mat;

    [Range(0, 0.5f)]
    public float intensityNormal;
    public float pixelsX;
    public float pixelsY;
    public float speedGlitch;
    public float speedLines;
    [Range(0, 1f)]
    public float numberOfLines;
    [Range(0, 1f)]
    public float opacityLines;
    [Range(0, 1f)]
    public float grayscaleOpacity;
    [Range(0, 1f)]
    public float pixelOpacity;
    public Color grayscaleColor;

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
        mat.SetTexture("_Texture", pencilStroke);
        mat.SetTexture("_Texture1", pencilStroke);
        mat.SetColor("_GrayscaleColor", grayscaleColor);
        mat.SetFloat("_IntensityNormal", intensityNormal);
        mat.SetFloat("_PixelsX", pixelsX);
        mat.SetFloat("_PixelsY", pixelsY);
        mat.SetFloat("_SpeedGlitch", speedGlitch);
        mat.SetFloat("_SpeedLines", speedLines);
        mat.SetFloat("_Numberoflines", numberOfLines);
        mat.SetFloat("_OpacityLines", opacityLines);
        mat.SetFloat("_PixelOpacity", pixelOpacity);
        mat.SetFloat("_GrayscaleOpacity", grayscaleOpacity);
    }
}
