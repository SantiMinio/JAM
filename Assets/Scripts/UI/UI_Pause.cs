using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : MonoBehaviour
{

    public void Action_Resume()
    {
        PauseManager.instance.ResumeMenuUI();
    }

    public void Action_Settings()
    {

    }

    public void Action_MainMenu()
    {
        SceneLoader.Load(0);
    }
}
