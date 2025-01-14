using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayers;

    [SerializeField] private float feedbackTime;
    [SerializeField] Animator anim = null;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            Main.instance.eventManager.TriggerEvent(GameEvents.HotelArrive);
            StartCoroutine(WaitToFeedback());
        }
    }

    IEnumerator WaitToFeedback()
    {
        float timer = 0;
        anim.SetBool("fade", true);
        while (timer < feedbackTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(SaveData.saveData.gameObject);
        SceneLoader.Load(0);
        
    }
}
