using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectable : MonoBehaviour
{
    public GameObject particlePrefab;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==20)
        {
            GameObject particles = Instantiate(particlePrefab, transform.position, transform.rotation);
            particles.transform.parent = null;
            Destroy(gameObject);
        }
    }

}
