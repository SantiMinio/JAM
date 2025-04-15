using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoInteractable : Interactable
{
    [SerializeField] PhotoSettings photoToRepresent;


    private void Start()
    {
        PhotoManager.Instance.SpawnPhotoModel(photoToRepresent);
    }

    public override void OnEnterInteract(Interactor interactor)
    {
    }

    public override void OnExitInteract(Interactor interactor)
    {
    }

    public override void OnInteract(Interactor interactor)
    {
        PhotoManager.Instance.OpenPhotoWindow(photoToRepresent);
    }
}
