using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : PageSelector<SettingPanel>
{
    //[SerializeField] Animator prevAnim = null;
    //[SerializeField] Animator nextAnim = null;

    [SerializeField] TextMeshProUGUI mainText;

    protected override void OnOpen()
    {
    }

    protected override void OnClose()
    {
        myPanels[currentIndex].panel.Close();
    }

    protected override void OnSetNewPanel(int lastIndex, int _currentIndex)
    {
        if (lastIndex > -1)
        {
            myPanels[lastIndex].panel.Close();

            //if ((lastIndex < _currentIndex && !(lastIndex == 0 && _currentIndex == myPanels.Length -1)) || (lastIndex > _currentIndex && (lastIndex == myPanels.Length - 1 && _currentIndex == 0)))
            //{
            //    NewSoundFX.PlaySound("ui_SettingsCardSwap");
            //    nextAnim.Play("StartRight");
            //}
            //else
            //{
            //    NewSoundFX.PlaySound("ui_SettingsCardSwap");
            //    prevAnim.Play("StartLeft");
            //}
        }

        myPanels[_currentIndex].panel.Open();


        mainText.text = myPanels[_currentIndex].panelID;
    }
}

[System.Serializable]
public struct SettingPanel
{
    public string panelID;
    public UIPanelTransition panel;
}
