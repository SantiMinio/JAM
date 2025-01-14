using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevelopTools;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main instance { get; private set; }

    public EventManager eventManager = new EventManager();

    public DJ_Handler djHandler;

    [SerializeField] CharacterBase charOne = null;
    [SerializeField] CharacterBase charTwo = null;

    [SerializeField] CheckpointManager checkpointManager = null;

    [SerializeField] Grayscale_Post_Process pp = null;

    [SerializeField] float restartTime = 3;
    Vector3 checkpointPosition = Vector3.zero;

    const string SaveDataName = "SaveData";
    SavedClass save;

    private void Awake()
    {
        instance = this;
        eventManager.SubscribeToEvent(GameEvents.CharactersSeparate, RestartGame);
        eventManager.SubscribeToEvent(GameEvents.OneCharDie, RestartGame);
        StartCoroutine(StartGame());
    }

    private void Start()
    {
        checkpointPosition = SaveData.saveData.checkpointPosition;

        if(checkpointPosition != Vector3.zero && checkpointManager.CheckIfCheckpoint(checkpointPosition))
        {
            charOne.transform.position = checkpointPosition;
            charTwo.transform.position = checkpointPosition + Vector3.right;
        }
    }

    IEnumerator StartGame()
    {
        pp.fade_in_out = 0;
        while (pp.fade_in_out<1)
        {
            pp.fade_in_out += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        pp.fade_in_out = 1;
    }

    public CharacterBase GetHusband() => charTwo;
    public CharacterBase GetWife() => charOne;

    public CharacterBase[] GetCharacters() => new CharacterBase[2] { charOne, charTwo };

    void RestartGame()
    {
        StartCoroutine(DelayToRestart());
    }

    IEnumerator DelayToRestart()
    {
        yield return new WaitForSeconds(restartTime);

        while (pp.fade_in_out > 0)
        {
            pp.fade_in_out -= Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }
        SceneLoader.Load(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetCheckpoint(Vector3 checkpointPos)
    {
        if (checkpointPosition == checkpointPos) return;

        checkpointPosition = checkpointPos;
        SaveData.saveData.checkpointPosition = checkpointPosition;
        //BinarySerialization.Serialize(SaveDataName, save);
    }
}
