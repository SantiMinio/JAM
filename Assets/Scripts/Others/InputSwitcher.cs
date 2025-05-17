using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.Users;

public class InputSwitcher : MonoBehaviour
{
    public static InputSwitcher instance { get; private set; }

    public PlayerInput[] inputs = null;
    [SerializeField] string keyboardScheme = null;
    [SerializeField] string joystickScheme = null;
    [SerializeField] string keyboardBothScheme = null;
    [SerializeField] string JoystickBothScheme = null;

    public event Action OnChangeJoystick;
    public event Action OnChangeKeyboard;

    public bool isJoystick { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.parent = null;
        instance = this;
        DontDestroyOnLoad(this);
        FindNewInput();
        if (inputs[0].currentControlScheme == joystickScheme || inputs[0].currentControlScheme == JoystickBothScheme)
        {
            OnChangeJoystick?.Invoke();
            isJoystick = true;
        }
        else if (inputs[0].currentControlScheme == keyboardScheme || inputs[0].currentControlScheme == keyboardBothScheme)
        {
            OnChangeKeyboard?.Invoke();
            isJoystick = false;
        }
    }

    private void Start()
    {
        //FindNewInput();

        if (inputs[0].currentControlScheme == joystickScheme || inputs[0].currentControlScheme == JoystickBothScheme)
        {
            OnChangeJoystick?.Invoke();
            isJoystick = true;
        }
        else if (inputs[0].currentControlScheme == keyboardScheme || inputs[0].currentControlScheme == keyboardBothScheme)
        {
            OnChangeKeyboard?.Invoke();
            isJoystick = false;
        }

        SceneLoader.Instance.OnEndLoadScene += FindNewInput;
    }

    //private void Update()
    //{
    //    if(input == null)
    //    {
    //        input = FindObjectOfType<PlayerInput>();
    //        return;
    //    }

    //    if (input.currentControlScheme == null) return;

    //    if (!isJoystick && input.currentControlScheme == joystickScheme)
    //    {
    //        OnChangeJoystick?.Invoke();
    //        isJoystick = true;
    //    }
    //    else if(isJoystick && input.currentControlScheme == keyboardScheme)
    //    {
    //        OnChangeKeyboard?.Invoke();
    //        isJoystick = false;
    //    }
    //}

    public void AddToAction(string actionName, Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback)
    {
        inputs[0].actions.FindAction(actionName).started += callback;
    }

    public void RemoveToAction(string actionName, Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback)
    {
        inputs[0].actions.FindAction(actionName).started -= callback;
    }

    public void AddToAllAction(string actionName, Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].actions.FindAction(actionName).started += callback;
        }
    }

    public void RemoveToAllAction(string actionName, Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].actions.FindAction(actionName).started -= callback;
        }
    }

    public void FindNewInput()
    {
        inputs = PlayerInput.all.ToArray();

        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].user.UnpairDevices();
            InputUser.PerformPairingWithDevice(Keyboard.current, inputs[i].user);
            inputs[i].user.ActivateControlScheme(keyboardBothScheme);
        }
        //    InputUser.PerformPairingWithDevice(Keyboard.current, inputs[0].user);
        //    inputs[0].user.ActivateControlScheme(keyboardBothScheme);
        //InputUser.PerformPairingWithDevice(Keyboard.current, inputs[1].user);
        //inputs[1].user.ActivateControlScheme(keyboardBothScheme);
    }

    public void SetPlayerSchema(string schema, InputDevice device, int player)
    {
        if (player >= inputs.Length) return;

        inputs[player].user.UnpairDevices();
        InputUser.PerformPairingWithDevice(device, inputs[player].user);
        inputs[player].user.ActivateControlScheme(schema);
    }
    
    public void EnableInput(int player)
    {
        inputs[player].enabled = true;
    }

    public void DisableInput(int player)
    {
        inputs[player].enabled = false;
    }
}
