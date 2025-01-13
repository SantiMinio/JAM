using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowIndexerSettings : DropdownSettings
{
    protected override void Awake()
    {
        dropdown.SetItems(new string[] { ShadowQuality.Disable.ToString(), ShadowQuality.HardOnly.ToString(), ShadowQuality.All.ToString() }, (int)QualitySettings.shadows);
        base.Awake();
    }

    protected override void ChangeValue(int value)
    {
        QualitySettings.shadows = (ShadowQuality)value;
    }
}
