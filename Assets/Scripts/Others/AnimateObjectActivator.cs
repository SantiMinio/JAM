using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjectActivator : MonoBehaviour
{
     public ActivableObject targetActivableObject;

     public void ActivateObject()
     {
          if(targetActivableObject != null)
               targetActivableObject.Activate();
     }

     public void DeactivateObject()
     {
          if(targetActivableObject != null)
               targetActivableObject.Deactivate();
     }
}
