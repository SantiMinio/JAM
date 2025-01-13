using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityIndexSettings : DropdownSettings
{
    [SerializeField] DropdownSettings shadows = null;
    [SerializeField] DropdownSettings vsync = null;
    [SerializeField] DropdownSettings antialiasing = null;

    protected override void Awake()
    {
        dropdown.SetItems(QualitySettings.names, QualitySettings.GetQualityLevel());
        base.Awake();
    }

    protected override void ChangeValue(int value)
    {
        QualitySettings.SetQualityLevel(value);

        vsync.SetIndex(QualitySettings.vSyncCount);
        shadows.SetIndex((int)QualitySettings.shadows);
        antialiasing.SetIndex(MSAAIndexerSettings.GetMSAIndex(QualitySettings.antiAliasing));
    }
}
