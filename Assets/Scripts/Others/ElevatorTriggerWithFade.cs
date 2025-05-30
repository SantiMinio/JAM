using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ElevatorTriggerWithFade : MonoBehaviour
{
    public Transform[] Doors;
    public Transform[] DoorsPos;
    public Transform elevator;
    public Transform vines;
    public Vector3 targetPosition;
    public Vector3 vinesScale;
    public float speed = 2f;
    public string playerTag = "Player";
    public string nextSceneName = "NextScene";

    public Image fadeCanvasGroup;  // Asigná el CanvasGroup del fade aquí
    public float fadeDuration = 2f;
    private HashSet<GameObject> playersInside = new HashSet<GameObject>();
    private bool elevatorMoving = false;
    private bool sceneLoading = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playersInside.Add(other.gameObject);
            CheckPlayers();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playersInside.Remove(other.gameObject);
        }
    }

    void CheckPlayers()
    {
        if (playersInside.Count >= 2 && !elevatorMoving)
        {
            StartCoroutine(MoveElevatorWithFade());
        }
    }

    IEnumerator MoveElevatorWithFade()
    {
        elevatorMoving = true;

        // Iniciar el fade
        StartCoroutine(FadeToBlack());

        // Mover el elevador mientras ocurre el fade
        while (Vector3.Distance(elevator.position, targetPosition) > 0.05f)
        {
            vines.position = Vector3.MoveTowards(vines.position, targetPosition, speed * Time.deltaTime);
            vines.localScale =Vector3.Lerp(vines.localScale, vinesScale,speed*Time.deltaTime);
            elevator.position = Vector3.MoveTowards(elevator.position, targetPosition, speed * Time.deltaTime);
            Doors[0].position = Vector3.MoveTowards(Doors[0].position, DoorsPos[0].position,speed * Time.deltaTime);
            Doors[1].position = Vector3.MoveTowards(Doors[1].position, DoorsPos[1].position, speed* Time.deltaTime);
            yield return null;
        }
      

            if (vines.localScale.magnitude <= vinesScale.magnitude)
        {
            vines.gameObject.SetActive(false);  
        }
        elevator.position = targetPosition;
        Doors[0].position=DoorsPos[0].position;
        Doors[1].position=DoorsPos[1].position;
        // Esperar a que el fade termine
        yield return new WaitForSeconds(fadeDuration);

        if (!sceneLoading)
        {
            sceneLoading = true;
            SceneManager.LoadScene(nextSceneName);
        }
    }

    IEnumerator FadeToBlack()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadeCanvasGroup.color=new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
