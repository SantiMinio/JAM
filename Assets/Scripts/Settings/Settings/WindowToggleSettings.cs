using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class WindowToggleSettings : ToggleSettings
{
    [SerializeField] CanvasGroup group = null;
    [SerializeField] ResDropdownSettings dropdown = null;

    private void Start()
    {
        if (toggle.isOn)
        {
            dropdown.SetResolutionValue();
            group.alpha = 1;
            group.interactable = true;
        }
        else
        {
            Screen.SetResolution(Screen.mainWindowDisplayInfo.width, Screen.mainWindowDisplayInfo.height, true);
            group.alpha = 0;
            group.interactable = false;
        }
    }

    protected override void ChangeValue(bool value)
    {
        Screen.fullScreen = !value;
        if (!value)
        {
            Screen.SetResolution(Screen.mainWindowDisplayInfo.width, Screen.mainWindowDisplayInfo.height, true);
            group.alpha = 0;
            group.interactable = false;
        }
        else
        {
            dropdown.SetResolutionValue();
            group.alpha = 1;
            group.interactable = true;
        }
    }
}
