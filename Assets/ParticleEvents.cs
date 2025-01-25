using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEvents : MonoBehaviour
{  
    CharacterAction Action;
    // Start is called before the first frame update
    void Start()
    {
        Action=GetComponentInParent<CharacterAction>();
        
    }

    public void ActivateEffect(int index)
    {
        Action.actionParticles[index].gameObject.SetActive(true);
    }


   
}
