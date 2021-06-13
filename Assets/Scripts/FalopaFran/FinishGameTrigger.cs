using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayers;

    [SerializeField] private float feedbackTime;
    Grayscale_Post_Process pp;

    private void Start()
    {
        pp = FindObjectOfType<Grayscale_Post_Process>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((triggerLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("entro");
            Main.instance.eventManager.TriggerEvent(GameEvents.HotelArrive);
            StartCoroutine(WaitToFeedback());
            UIManager.instance.gameObject.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitToFeedback()
    {
        float timer = 0;
        while (timer < feedbackTime)
        {
            timer += Time.deltaTime;
            pp.heartEffect = timer / feedbackTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(SaveData.saveData.gameObject);
        SceneManager.LoadScene("FranMapRemake1 1");
        
    }
}
