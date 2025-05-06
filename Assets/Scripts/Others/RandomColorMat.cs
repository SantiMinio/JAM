using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorMat : MonoBehaviour
{
    public int indexMat;
    // Start is called before the first frame update
    void Start()
    { 
        // Crear una instancia del material para no afectar a todos los NPCs
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.materials[indexMat] = new Material(renderer.materials[indexMat]); // Clonar material
            renderer.materials[indexMat].color = new Color(Random.value, Random.value, Random.value);
        }

    }

  
}
