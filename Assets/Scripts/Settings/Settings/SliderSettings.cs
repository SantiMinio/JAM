using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SliderSettings : SettingThing
{
    [SerializeField] protected Slider slider = null;


    protected virtual void Awake()
    {
        DefaultValue = slider.value;
        slider.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<float>(OnModifyValue));
    }

    public override void LoadData(SettingsData data)
    {
        float value = (float)data.GetValue(ID);

        if (value == -1)
            SaveData(data);
        else
            slider.value = value;
    }

    public void OnModifyValue(float value)
    {
        SaveData(Settings.Instance.GetData());
        ChangeValue(value);
    }

    public override void SaveData(SettingsData data)
    {
        data.ModifySettings(ID, slider.value);
    }

    protected abstract void ChangeValue(float value);


    public override void ResetToDefaultValue()
    {
        slider.value = DefaultValue;
    }
}
