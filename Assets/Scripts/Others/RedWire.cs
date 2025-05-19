using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RedWire : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float maxDistance = 10f; // distancia máxima antes de que la línea sea lo más fina posible
    public float maxWidth = 0.1f;   // grosor máximo cuando están cerca
    public float minWidth = 0.01f;  // grosor mínimo cuando están lejos
    
    private LineRenderer lineRenderer;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (pointA != null && pointB != null)
        {
           
            Vector3 posA = pointA.position;
            Vector3 posB = pointB.position;
            float distance = Vector3.Distance(posA, posB);
            if (distance < 3)
            {
                lineRenderer.enabled = false;
            }
            else
            {
                lineRenderer.enabled = enabled;
            }
                // Interpolar grosor entre maxWidth y minWidth
                float t = Mathf.Clamp01(distance / maxDistance);
            float width = Mathf.Lerp(maxWidth, minWidth, t);
           
            // Asignar posiciones
            lineRenderer.SetPosition(0, posA);
            lineRenderer.SetPosition(1, posB);

            // Ajustar grosor en ambos extremos
            lineRenderer.widthCurve = new AnimationCurve(
                new Keyframe(0, width),
                new Keyframe(1, width)
            );
        }
    }
}
