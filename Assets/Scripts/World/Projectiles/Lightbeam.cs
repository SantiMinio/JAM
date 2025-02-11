using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(LineRenderer))]
public class Lightbeam : MonoBehaviour, IPause
{
    [SerializeField] int reflections = 0;
    [SerializeField] float maxLength = 100f;

    [SerializeField] private Transform lineOrigin;

    [SerializeField] private Damager dmg;

    [SerializeField] private ParticleSystem endpoint_feedback_pf, endpoint_feedback;

    [SerializeField] float anticipationTime = 1;
    
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LineRenderer anticipationLineRenderer;
    Ray ray;
    RaycastHit hit;

    bool paused;

    private void Start()
    {
        PauseManager.instance.AddToPause(this);
        endpoint_feedback.gameObject.SetActive(false);
    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }

    public void SetActive(bool value)
    {
        lineRenderer.enabled = value;
        endpoint_feedback.gameObject.SetActive(value);
    }

    private void Awake()
    {
        endpoint_feedback = Instantiate(endpoint_feedback_pf, transform);
        //endpoint_feedback.gameObject.SetActive(false);
    }

    public bool anticipation;
    public bool inLightbeam;
    float anticipationTimer;

    public void StartAnticipation()
    {
        anticipation = true;
        anticipationLineRenderer.enabled = true;
    }

    public void StartLightbeam()
    {
        inLightbeam = true;
        anticipation = false;
        anticipationTimer = 0;
        anticipationLineRenderer.enabled = false;
        lineRenderer.enabled = true;
        endpoint_feedback.gameObject.SetActive(true);
        Debug.Log("started");
    }

    public void StopLightbeam()
    {
        inLightbeam = false;
        anticipation = false;
        lineRenderer.enabled = false;
        anticipationLineRenderer.enabled = false;
        anticipationTimer = 0;
        Debug.Log("stopped");
        endpoint_feedback.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (paused) return;

        if(anticipation)
        {
            anticipationTimer += Time.deltaTime;
            anticipationLineRenderer.SetPosition(0, lineOrigin.position);
            anticipationLineRenderer.SetPosition(1, lineOrigin.position + transform.forward * maxLength);

            if(anticipationTimer >= anticipationTime)
                StartLightbeam();

        }


        if (!inLightbeam) return;


        ray = new Ray(lineOrigin.position, transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, lineOrigin.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            if(Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, dmg.rivalsMask, QueryTriggerInteraction.Collide))
            {
               
                var hiteable = hit.collider.GetComponent<DamageReceiver>();
                if (hiteable != null&&hiteable.gameObject.tag!="DestructibleWall")
                {
                    Vector3 myAttackDir = (transform.position - hiteable.transform.position).normalized;

                    dmg.knockbackModule.knockbackDir = myAttackDir;

                    hiteable.DoDamage(dmg);
                }
                
                
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                endpoint_feedback.transform.position = hit.point;
                
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
