using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusicChange : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayers;

    public int index;
    private void OnTriggerEnter(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            if (Main.instance.djHandler.currentAudioIndex != index)
            {
                Main.instance.djHandler.ChangeMusic(index);
            }

        }
    }
}
