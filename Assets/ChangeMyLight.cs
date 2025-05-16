using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; // Necesario para controlar la Timeline

public class ChangeMyLight : MonoBehaviour
{
    [Header("Player")]
    public string playerTag = "Player";

    [Header("Directional Light")]
    public Light directionalLight;
    public Color newLightColor = Color.red;
    public float newLightIntensity = 1.5f;

    [Header("Environment Lighting")]
    public bool affectAmbientLight = true;
    public Color ambientOffColor = Color.black;
    public float ambientOffIntensity = 0f;

    [Header("Transition")]
    public float transitionDuration = 1f;
    public bool restoreOnExit = true;

    [Header("Timeline")]
    public PlayableDirector timelineToPlay;

    private Color originalLightColor;
    private float originalLightIntensity;
    private Color originalAmbientColor;
    private float originalAmbientIntensity;
    public CameraFollow cam;

    private void Start()
    {
        if (directionalLight != null)
        {
            originalLightColor = directionalLight.color;
            originalLightIntensity = directionalLight.intensity;
        }

        if (affectAmbientLight)
        {
            originalAmbientColor = RenderSettings.ambientLight;
            originalAmbientIntensity = RenderSettings.ambientIntensity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && directionalLight != null)
        {
            cam.offset = new Vector3(0, 15.7f, -8);
            cam.smoothTime = 1;
            StopAllCoroutines();
            StartCoroutine(TransitionLighting(
                directionalLight.color, newLightColor,
                directionalLight.intensity, newLightIntensity,
                RenderSettings.ambientLight, ambientOffColor,
                RenderSettings.ambientIntensity, ambientOffIntensity
            ));
            if (timelineToPlay != null)
            {
                timelineToPlay.Play();
            }
        }
    }

    private System.Collections.IEnumerator TransitionLighting(
          Color fromLightColor, Color toLightColor,
          float fromLightIntensity, float toLightIntensity,
          Color fromAmbientColor, Color toAmbientColor,
          float fromAmbientIntensity, float toAmbientIntensity)
    {
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;

            if (directionalLight != null)
            {
                directionalLight.color = Color.Lerp(fromLightColor, toLightColor, t);
                directionalLight.intensity = Mathf.Lerp(fromLightIntensity, toLightIntensity, t);
            }

            if (affectAmbientLight)
            {
                RenderSettings.ambientLight = Color.Lerp(fromAmbientColor, toAmbientColor, t);
                RenderSettings.ambientIntensity = Mathf.Lerp(fromAmbientIntensity, toAmbientIntensity, t);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (directionalLight != null)
        {
            directionalLight.color = toLightColor;
            directionalLight.intensity = toLightIntensity;
        }

        if (affectAmbientLight)
        {
            RenderSettings.ambientLight = toAmbientColor;
            RenderSettings.ambientIntensity = toAmbientIntensity;
        }
    }
}