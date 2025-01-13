using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SettingThing : MonoBehaviour
{
    [SerializeField] protected string ID = "Default";

    public float DefaultValue { get; protected set; }

    public abstract void ResetToDefaultValue();

    public abstract void LoadData(SettingsData data);

    public abstract void SaveData(SettingsData data);

}
