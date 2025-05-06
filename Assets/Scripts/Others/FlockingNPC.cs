using System.Collections.Generic;
using UnityEngine;

public class FlockingNPC : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float cohesionWeight = 1.0f;
    public float separationWeight = 1.5f;
    public float avoidanceWeight = 2.0f;
   

    public float neighborRadius = 5f;
    public float separationDistance = 2f;
    public float avoidPlayerDistance = 6f;

    public List<Transform> players;
    public LayerMask npcLayer;
    [Header("Percepción del jugador")]
    public float visionRange = 15f;
    public float visionAngle = 120f;

    void Start()
    {
        foreach (var p in FindObjectsOfType<CharacterBase>())
        {
            players.Add(p.transform);
        }
        
    }

    void Update()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 avoidance = Vector3.zero;

        int neighborCount = 0;

        Collider[] neighbors = Physics.OverlapSphere(transform.position, neighborRadius, npcLayer);

        foreach (var neighbor in neighbors)
        {
            if (neighbor.gameObject == this.gameObject) continue;

            Vector3 toNeighbor = neighbor.transform.position - transform.position;
            float dist = toNeighbor.magnitude;

            cohesion += neighbor.transform.position;

            if (dist < separationDistance)
            {
                separation -= toNeighbor.normalized / dist; // alejarse con fuerza inversa a la distancia
            }

            neighborCount++;
        }

        if (neighborCount > 0)
        {
            cohesion = (cohesion / neighborCount - transform.position).normalized;
        }

        foreach (Transform player in players)
        {
            Vector3 toPlayer = transform.position - player.position;
            float dist = toPlayer.magnitude;

            if (dist < avoidPlayerDistance)
            {
                avoidance += toPlayer.normalized / dist;
            }
        }

        Vector3 moveDir =
            cohesion * cohesionWeight +
            separation * separationWeight +
            avoidance * avoidanceWeight;

        if (moveDir != Vector3.zero)
        {
            transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
            transform.forward = moveDir.normalized; // opcional: que mire hacia donde se mueve
        }
    }
}
