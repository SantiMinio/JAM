using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SameDeviceToggleSetting : ToggleSettings
{
    [SerializeField] DeviceSelectorDropdownSetting bothSelector = null;
    [SerializeField] DeviceSelectorDropdownSetting playerOneSelector = null;
    [SerializeField] DeviceSelectorDropdownSetting playerTwoSelector = null;

    Dictionary<DeviceSelectorDropdownSetting, Action<ID_Schema>> decisionTree = new Dictionary<DeviceSelectorDropdownSetting, Action<ID_Schema>>();

    bool IsControlledWithSameDevice => toggle.isOn;

    protected override void Awake()
    {
        decisionTree.Add(bothSelector, (x) => { if (IsControlledWithSameDevice) SetBothPlayers(x); });
        decisionTree.Add(playerOneSelector, (x) => { if (!IsControlledWithSameDevice) SetPlayer(x, 0); });
        decisionTree.Add(playerTwoSelector, (x) => { if (!IsControlledWithSameDevice) SetPlayer(x, 1); });

        base.Awake();
    }

    public void SetNewDevice(ID_Schema schema, DeviceSelectorDropdownSetting changer)
    {
        decisionTree[changer].Invoke(schema);
    }

    protected override void ChangeValue(bool value)
    {
        if(value)
        {
            bothSelector.OpenClose(true);
            var nav = new UnityEngine.UI.Navigation();
            nav.mode = UnityEngine.UI.Navigation.Mode.Explicit;
            nav.selectOnDown = bothSelector.GetComponentInChildren<Button>();
            toggle.navigation = nav;
            playerOneSelector.OpenClose(false);
            playerTwoSelector.OpenClose(false);

            SetBothPlayers(bothSelector.RefreshDevices());
        }
        else
        {
            bothSelector.OpenClose(false);
            playerOneSelector.OpenClose(true);
            playerTwoSelector.OpenClose(true);
            var nav = new UnityEngine.UI.Navigation();
            nav.mode = UnityEngine.UI.Navigation.Mode.Explicit;
            nav.selectOnDown = playerOneSelector.GetComponentInChildren<Button>();
            toggle.navigation = nav;

            SetPlayer(playerOneSelector.RefreshDevices(),0);
            SetPlayer(playerTwoSelector.RefreshDevices(),1);
        }
    }

    void SetBothPlayers(ID_Schema schema)
    {
        InputSwitcher.instance.SetPlayerSchema(schema.Schema, schema.Device, 0);
        InputSwitcher.instance.SetPlayerSchema(schema.Schema, schema.Device, 1);
    }

    void SetPlayer(ID_Schema schema, int playerIndex)
    {
        InputSwitcher.instance.SetPlayerSchema(schema.Schema, schema.Device, playerIndex);
    }
}
