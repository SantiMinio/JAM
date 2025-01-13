using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VsyncIndexerSettings : DropdownSettings
{
    protected override void Awake()
    {
        dropdown.SetItems(new string[] { "Off", "Vsync 1" }, QualitySettings.vSyncCount);
        base.Awake();
    }

    protected override void ChangeValue(int value)
    {
        QualitySettings.vSyncCount = value;
    }
}
