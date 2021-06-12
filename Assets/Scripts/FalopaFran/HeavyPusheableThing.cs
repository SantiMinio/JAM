using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyPusheableThing : MonoBehaviour
{
    [SerializeField] private List<CharacterBase> _characterBases = new List<CharacterBase>();
    
    [SerializeField] private LayerMask triggerLayers;
    
    public bool TwoCharacterPushing { get; private set; }

    [SerializeField] private Rigidbody rb;
    
    private void OnTriggerStay(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            if(_characterBases.Contains(other.GetComponent<CharacterBase>())) return;
            
            _characterBases.Add(other.GetComponent<CharacterBase>());
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            if(!_characterBases.Contains(other.GetComponent<CharacterBase>())) return;
            
            _characterBases.Remove(other.GetComponent<CharacterBase>());
        }
    }


    private void Update()
    {
        TwoCharacterPushing = _characterBases.Count > 1;
        rb.isKinematic = !TwoCharacterPushing;
    }
}
