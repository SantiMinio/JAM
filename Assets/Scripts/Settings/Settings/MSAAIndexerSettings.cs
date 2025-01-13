using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MSAAIndexerSettings : DropdownSettings
{
    public static int[] MSAA = new int[] { 0, 2, 4, 8 };

    public static int GetMSAIndex(int msaa)
    {
        return MSAA.Where(x => x == msaa).FirstOrDefault();
    }

    protected override void Awake()
    {
        dropdown.SetItems(new string[] { "settings.Off", "MSAA 2x", "MSAA 4x", "MSAA 8x" }, GetMSAIndex(QualitySettings.antiAliasing));
        base.Awake();
    }

    protected override void ChangeValue(int value)
    {
        QualitySettings.antiAliasing = value;
    }
}
