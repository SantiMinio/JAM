using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butonCoso : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("pressed", true);
        
    }
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("pressed", false);
    }
}
