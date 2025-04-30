using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    List<Interactable> interactables = new List<Interactable>();

    public void EnterCollect(Interactable collectabe)
    {
        collectabe.OnEnterInteract(this);
        if(!interactables.Contains(collectabe)) interactables.Add(collectabe);
    }

    public void ExitCollect(Interactable collectabe)
    {
        collectabe.OnEnterInteract(this);
        if (interactables.Contains(collectabe)) interactables.Remove(collectabe);
    }

    public bool Interact()
    {
        if (interactables.Count <= 0) return false;

        GetNearest().OnInteract(this);

        return true;
    }

    public Interactable GetNearest()
    {
        return interactables.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First();
    }
}
