using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] SettingsMenu settings = null;

    public void Action_Resume()
    {
        PauseManager.instance.ResumeMenuUI();
    }

    public void Action_Settings()
    {
        settings.Open();
    }

    public void Action_MainMenu()
    {
        SceneLoader.Load(0);
    }
    public void Action_Restart()
    {
        SaveData.saveData.checkpointPosition = Vector3.zero;
        SceneLoader.Load(SceneManager.GetActiveScene().name);
    }
}
