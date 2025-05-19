using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RedWire : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float maxDistance = 10f; // distancia m�xima antes de que la l�nea sea lo m�s fina posible
    public float maxWidth = 0.1f;   // grosor m�ximo cuando est�n cerca
    public float minWidth = 0.01f;  // grosor m�nimo cuando est�n lejos
    
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
