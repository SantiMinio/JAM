using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ResDropdownSettings : DropdownSettings
{
    [SerializeField] List<string> resolutions = new List<string>();
    List<Resolution> currentResolutions = new List<Resolution>();

    protected override void Awake()
    {
        int current = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            string[] res = resolutions[i].Split('x');

            if (int.Parse(res[0]) <= Screen.mainWindowDisplayInfo.width && int.Parse(res[1]) <= Screen.mainWindowDisplayInfo.height)
            {
                var newResolution =  new Resolution();
                newResolution.width = int.Parse(res[0]);
                newResolution.height = int.Parse(res[1]);
                currentResolutions.Add(newResolution);
                if (int.Parse(res[0]) == Screen.currentResolution.width && int.Parse(res[1]) <= Screen.currentResolution.height)
                {
                    current = currentResolutions.Count - 1;
                }
            }
        }

        dropdown.SetItems(currentResolutions.Select(x => x.width + "x" + x.height).ToArray(), current);

        base.Awake();
    }

    protected override void ChangeValue(int index)
    {
        if(!Screen.fullScreen)
            Screen.SetResolution(currentResolutions[index].width, currentResolutions[index].height, false);
    }

    public void SetResolutionValue()
    {
        Screen.SetResolution(currentResolutions[dropdown.CurrentSelectedItem].width, currentResolutions[dropdown.CurrentSelectedItem].height, false);
    }
}
