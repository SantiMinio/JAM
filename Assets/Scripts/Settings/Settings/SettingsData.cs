using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsData
{
    public List<SettingThingData> settingsData = new List<SettingThingData>();

    public void ModifySettings(string ID, float value)
    {
        SettingThingData result = null;


        for (int i = 0; i < settingsData.Count; i++)
        {
            if(settingsData[i].ID == ID)
            {
                result = settingsData[i];
                result.value = value;
                break;
            }
        }

        if(result == null)
        {
            result = new SettingThingData();
            result.ID = ID;
            result.value = value;
            settingsData.Add(result);
        }
    }

    public float GetValue(string ID)
    {
        SettingThingData result = null;

        for (int i = 0; i < settingsData.Count; i++)
        {
            if (settingsData[i].ID == ID)
            {
                result = settingsData[i];
                break;
            }
        }

        return result == null ? -1 : result.value;
    }
}

[Serializable]
public class SettingThingData
{
    public string ID;
    public float value;
}