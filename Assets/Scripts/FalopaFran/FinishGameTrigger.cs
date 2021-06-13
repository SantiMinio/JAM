using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayers;

    [SerializeField] private float feedbackTime;
    private void OnTriggerEnter(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("entro");
            Main.instance.eventManager.TriggerEvent(GameEvents.HotelArrive);
            StartCoroutine(WaitToFeedback());
        }
    }

    IEnumerator WaitToFeedback()
    {
        yield return new WaitForSeconds(feedbackTime);

        SceneManager.LoadScene("FranMapRemake1 1");
    }
}
