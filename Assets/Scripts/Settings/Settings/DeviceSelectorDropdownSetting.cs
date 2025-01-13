using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using System.Linq;

public class DeviceSelectorDropdownSetting : DropdownSettings
{
    List<ID_Schema> currentPosibleDevices = new List<ID_Schema>();

    [SerializeField] SameDeviceToggleSetting deviceConnector;
    [SerializeField] string keyboardSchema = "Keyboard";
    [SerializeField] string joystickSchema = "Joystick";
    [SerializeField] CanvasGroup myGroup = null;

    protected override void Awake()
    {
        OnDeviceConected();

        dropdown.OnOpenDropdown += OnDeviceConected;
        base.Awake();
    }

    void OnDeviceConected()
    {
        currentPosibleDevices.Clear();
        currentPosibleDevices.Add(new ID_Schema("Keyboard", keyboardSchema, Keyboard.current));

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            currentPosibleDevices.Add(new ID_Schema("Joystick " + (i + 1).ToString(), joystickSchema, Gamepad.all[i]));
        }
        int current = dropdown.CurrentSelectedItem;

        if (current >= currentPosibleDevices.Count)
            current = 0;

        dropdown.SetItems(currentPosibleDevices.Select(x => x.ID).ToArray(), current);
    }

    protected override void ChangeValue(int index)
    {
        deviceConnector.SetNewDevice(currentPosibleDevices[index], this);
    }

    public ID_Schema RefreshDevices()
    {
        OnDeviceConected();

        return currentPosibleDevices[dropdown.CurrentSelectedItem];
    }

    public void OpenClose(bool open)
    {
        myGroup.interactable = open;
        myGroup.blocksRaycasts = open;
        myGroup.alpha = open ? 1 : 0;
    }
}
 public struct ID_Schema
{
    public string ID;
    public string Schema;
    public InputDevice Device;

    public ID_Schema(string iD, string schema, InputDevice device)
    {
        ID = iD;
        Schema = schema;
        Device = device;
    }
}