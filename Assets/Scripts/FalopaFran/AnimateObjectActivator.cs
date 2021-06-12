using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjectActivator : MonoBehaviour
{
     public ActivableObject targetActivableObject;

     public void ActivateObject()
     {
          targetActivableObject.Activate();
     }

     public void DeactivateObject()
     {
          targetActivableObject.Deactivate();
     }
}
