using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ToggleSettings : SettingThing
{
    [SerializeField] protected Toggle toggle = null;

    protected virtual void Awake()
    {
        DefaultValue = toggle.isOn ? 1f : 0f;
        toggle.onValueChanged.AddListener(OnModifyValue);
    }

    public override void LoadData(SettingsData data)
    {
        float value = (float)data.GetValue(ID);

        if (value == -1)
            SaveData(data);
        else
            toggle.isOn = value == 0 ? false : true;
    }

    public void OnModifyValue(bool value)
    {
        SaveData(Settings.Instance.GetData());
        ChangeValue(value);
        Settings.Instance.SaveSettings();
    }

    public override void SaveData(SettingsData data)
    {
        data.ModifySettings(ID, toggle.isOn ? 1 : 0);
    }

    public override void ResetToDefaultValue()
    {
        toggle.isOn = DefaultValue == 0 ? false : true;
    }

    protected abstract void ChangeValue(bool value);
}
