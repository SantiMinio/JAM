using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }

    [SerializeField] SettingThing[] mySettings = new SettingThing[0];

    SettingsData data = new SettingsData();

    public const string SettingsPath = "Settings";

    private void Start()
    {
        Instance = this;

        mySettings = GetComponentsInChildren<SettingThing>();

        if (JSONSerialization.IsFileExist(SettingsPath))
        {
            JSONSerialization.Deserialize(SettingsPath, data);
            for (int i = 0; i < mySettings.Length; i++)
            {
                mySettings[i].LoadData(data);
            }
        }
        else
        {
            for (int i = 0; i < mySettings.Length; i++)
            {
                mySettings[i].SaveData(data);
            }
        }
        SaveSettings();
    }

    public void SaveSettings()
    {
        JSONSerialization.Serialize(SettingsPath, data, false);
    }

    public SettingsData GetData()
    {
        return data;
    }

    public void ResetToDefaultValues()
    {
        for (int i = 0; i < mySettings.Length; i++)
        {
            mySettings[i].ResetToDefaultValue();
        }
    }
}
