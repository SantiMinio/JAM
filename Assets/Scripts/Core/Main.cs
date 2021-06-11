using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevelopTools;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main instance { get; private set; }

    public EventManager eventManager = new EventManager();

    [SerializeField] float restartTime = 3;


    private void Awake()
    {
        instance = this;
        eventManager.SubscribeToEvent(GameEvents.CharactersSeparate, RestartGame);
    }


    void RestartGame()
    {
        StartCoroutine(DelayToRestart());
    }

    IEnumerator DelayToRestart()
    {
        yield return new WaitForSeconds(restartTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
