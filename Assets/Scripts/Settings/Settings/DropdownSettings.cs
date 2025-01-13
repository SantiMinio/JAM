using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DropdownSettings : SettingThing
{
    [SerializeField] protected CustomDropdown dropdown = null;

    protected virtual void Awake()
    {
        DefaultValue = dropdown.CurrentSelectedItem;
        dropdown.OnSelectItem.AddListener(OnModifyValue);
    }

    public override void LoadData(SettingsData data)
    {
        float value = (float)data.GetValue(ID);

        if (value == -1)
            SaveData(data);
        else
            dropdown.SelectItem((int)value < dropdown.ItemsAmount ? (int)value : 0);
    }

    public void OnModifyValue(int index)
    {
        SaveData(Settings.Instance.GetData());
        ChangeValue(index);
    }

    public override void SaveData(SettingsData data)
    {
        data.ModifySettings(ID, dropdown.CurrentSelectedItem);
    }

    public void SetIndex(int index)
    {
        dropdown.SetIndex(index);
    }

    public override void ResetToDefaultValue()
    {
        SetIndex((int)DefaultValue);
    }

    protected abstract void ChangeValue(int index);
}
