using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
