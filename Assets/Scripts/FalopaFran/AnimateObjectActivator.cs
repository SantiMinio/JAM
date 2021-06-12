using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjectActivator : MonoBehaviour
{
     public AnimatedObject targetAnimatedObject;

     public void ActivateObject()
     {
          targetAnimatedObject.Activate();
     }

     public void DeactivateObject()
     {
          targetAnimatedObject.Deactivate();
     }
}
