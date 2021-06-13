using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Buttons : MonoBehaviour
{
    public GameObject[] names = new GameObject[4];
    [SerializeField] GameObject mainScene = null;
    [SerializeField] GameObject levelSelector = null;
    [SerializeField] GameObject settingsScreen = null;
    [SerializeField] Image tutoImg = null;
    [SerializeField] Sprite[] tuto = new Sprite[0];
     
    [SerializeField] AudioClip exitSound = null;
    [SerializeField] Transform[] cameraPos = new Transform[0];
    [SerializeField] float cameraSpeed = 3;

    int currentIndex = -1;
    int cameraIndex;

    private void Awake()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].SetActive(false);
        }
    }

    public void DisplayTuto(int dir)
    {
        tutoImg.gameObject.SetActive(true);
        currentIndex += dir;
        if (currentIndex < 0)
        {
            tutoImg.gameObject.SetActive(false);
            return;
        }
        else if(currentIndex >= tuto.Length)
        {
            SceneManager.LoadScene(1);
            return;
        }

        tutoImg.sprite = tuto[currentIndex];
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Options()
    {
        settingsScreen.SetActive(true);
        mainScene.SetActive(false);
    }
    public void Credits()
    {
        mainScene.SetActive(false);
        for (int i = 0; i < names.Length; i++)
        {
            names[i].SetActive(true);
        }
    }

    public void GoToCredits()
    {
        cameraIndex += 1;
        if (cameraIndex >= cameraPos.Length)
        {
            cameraIndex = 0;
            mainScene.SetActive(true);
            for (int i = 0; i < names.Length; i++)
            {
                names[i].SetActive(false);
            }
        }
    }
    public void ExitGame()
    {
        StartCoroutine(ExitDelay());
    }

    IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

    private void Update()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos[cameraIndex].position, Time.deltaTime * cameraSpeed);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraPos[cameraIndex].rotation, Time.deltaTime * cameraSpeed);
    }

}
