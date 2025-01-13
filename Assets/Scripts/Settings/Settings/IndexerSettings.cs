using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IndexerSettings : SettingThing
{
    [SerializeField] protected ImageSelectorUI index = null;

    protected virtual void Awake()
    {
        index.OnChangeIndex += OnModifyValue;
    }

    public override void LoadData(SettingsData data)
    {
        float value = (float)data.GetValue(ID);

        if (value == -1)
            SaveData(data);
        else
            index.SetIndex((int)value);
    }

    public void SetIndex(int index)
    {
        this.index.SetIndex(index);
    }

    public void OnModifyValue(int value, int oldValue)
    {
        SaveData(Settings.Instance.GetData());
        ChangeValue(value);
        Settings.Instance.SaveSettings();
    }

    public override void SaveData(SettingsData data)
    {
        data.ModifySettings(ID, index.CurrentIndex);
    }

    protected abstract void ChangeValue(float value);
}
