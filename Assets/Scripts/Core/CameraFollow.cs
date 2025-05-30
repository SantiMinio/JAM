using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IPause
{
    public List<CharacterBase> ants = new List<CharacterBase>();
    private Camera _cam;
    public Vector3 offset;
    public float smoothTime = 5f;
    public float minZoom = 40;
    public float maxZoom = 10;
    public float zoomLimiter;
    private Vector3 velocity;

    private Vector3 initialPosition;

    // Shake variables
    private float initialShakeDuration = 0f; // para guardar la duración original
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.5f;
    private float dampingSpeed = 1.0f;

    bool paused;

    void Start()
    {
        _cam = Camera.main;
        ants.AddRange(GameObject.FindObjectsOfType<CharacterBase>());

        float newZoom = Mathf.Lerp(maxZoom, minZoom, GreatestDistance() / zoomLimiter);
        _cam.fieldOfView = newZoom;

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = newPosition;

        initialPosition = transform.localPosition;

        PauseManager.instance.AddToPause(this);
    }

    void LateUpdate()
    {
        if (paused || ants.Count == 0)
            return;

        Move();
        Zoom();

        ApplyShake();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 targetPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        initialPosition = transform.position; // Actualizamos la posición base para el shake
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GreatestDistance() / zoomLimiter);
        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, newZoom, Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        if (ants.Count == 1)
        {
            return ants[0].transform.position;
        }

        var bounds = new Bounds(ants[0].transform.position, Vector3.zero);
        foreach (var ant in ants)
        {
            bounds.Encapsulate(ant.transform.position);
        }

        return bounds.center;
    }

    float GreatestDistance()
    {
        var bounds = new Bounds(ants[0].transform.position, Vector3.zero);
        foreach (var ant in ants)
        {
            bounds.Encapsulate(ant.transform.position);
        }

        return bounds.size.x;
    }

    void ApplyShake()
    {
        if (shakeDuration > 0)
        {
            float shakeProgress = shakeDuration / initialShakeDuration; // 1 → 0
            float currentMagnitude = Mathf.Lerp(0f, shakeMagnitude, shakeProgress);

            transform.position = initialPosition + Random.insideUnitSphere * currentMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = initialPosition;
        }
    }

    // Método público para activar el shake
    public void TriggerShake(float duration = 0.3f, float magnitude = 0.5f)
    {
        shakeDuration = duration;
        initialShakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }
}
