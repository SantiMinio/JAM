using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevelopTools;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main instance { get; private set; }

    public EventManager eventManager = new EventManager();

    [SerializeField] CharacterBase charOne = null;
    [SerializeField] CharacterBase charTwo = null;

    [SerializeField] float restartTime = 3;
    Vector3 checkpointPosition = Vector3.zero;

    const string SaveDataName = "SaveData";
    SavedClass save;

    private void Awake()
    {
        instance = this;
        eventManager.SubscribeToEvent(GameEvents.CharactersSeparate, RestartGame);
    }

    private void Start()
    {
        if (BinarySerialization.IsFileExist(SaveDataName))
        {
            save = BinarySerialization.Deserialize<SavedClass>(SaveDataName);
            checkpointPosition = save.GetCheckPoint();
        }
        else
        {
            save = new SavedClass();
            BinarySerialization.Serialize(SaveDataName, save);
        }

        if(checkpointPosition != Vector3.zero)
        {
            charOne.transform.position = checkpointPosition;
            charTwo.transform.position = checkpointPosition + Vector3.right;
        }
    }

    public CharacterBase GetHusband() => charOne;
    public CharacterBase GetWife() => charTwo;

    public CharacterBase[] GetCharacters() => new CharacterBase[2] { charOne, charTwo };

    void RestartGame()
    {
        StartCoroutine(DelayToRestart());
    }

    IEnumerator DelayToRestart()
    {
        yield return new WaitForSeconds(restartTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetCheckpoint(Vector3 checkpointPos)
    {
        if (checkpointPosition == checkpointPos) return;

        checkpointPosition = checkpointPos;
        save.SaveCheckPoint(checkpointPosition);
        BinarySerialization.Serialize(SaveDataName, save);
    }
}
