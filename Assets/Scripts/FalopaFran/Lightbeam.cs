using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lightbeam : MonoBehaviour
{
    [SerializeField] int reflections = 0;
    [SerializeField] float maxLength = 100f;

    [SerializeField] private Transform lineOrigin;
    
    LineRenderer lineRenderer;
    Ray ray;
    RaycastHit hit;
    Vector3 direction;

    private bool isOn;

    public void SetActive(bool value)
    {
        lineRenderer.enabled = value;
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(!lineRenderer.enabled) return; 
        
        ray = new Ray(lineOrigin.position, transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, lineOrigin.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            if(Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                Debug.Log("aca entra");
                var hiteable = hit.collider.GetComponent<Hiteable>();
                if (hiteable != null)
                {
                    Debug.Log("seguro aca no");
                    if(hiteable.ImInvulnerable()) return;

                    Vector3 myAttackDir = (transform.position - hiteable.GetPosition()).normalized;

                    hiteable.GetHit(myAttackDir);
                }
                
                
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (hit.collider.tag != "Mirror")
                    break;
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }

    }
}
