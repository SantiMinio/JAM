using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public static class InputManager
{
    static bool IsJoystick;

    static Dictionary<string, KeyCode> KeyboardInputs = new Dictionary<string, KeyCode>()
    {
        { InputName.Jump, KeyCode.Space },
    };

    static Dictionary<string, KeyCode> JoystickInputs = new Dictionary<string, KeyCode>()
    {
        { InputName.Jump, KeyCode.Joystick1Button0 }
    };

    private static Dictionary<KeyEventButon, Func<KeyCode, bool>> ButtonEventDic = new Dictionary<KeyEventButon, Func<KeyCode, bool>>(3)
    {
        { KeyEventButon.KeyDown, Input.GetKeyDown },
        { KeyEventButon.Key, Input.GetKey },
        { KeyEventButon.KeyUp, Input.GetKeyUp }
    };

    public static bool GetInput(string inputName, KeyEventButon eventButton)
    {
        if (ButtonEventDic[eventButton](JoystickInputs[inputName]))
        {
            IsJoystick = true;
            return true;
        }
        else if (ButtonEventDic[eventButton](KeyboardInputs[inputName]))
        {
            IsJoystick = false;
            return true;
        }

        return false;
    }
}

public class InputName
{
    public const string Jump = "Input_Jump";
}

[Serializable] public class UnityEvFloat : UnityEvent<float> { }

[Serializable] public class UnityEvKeyButton : UnityEvent<KeyEventButon> { }
