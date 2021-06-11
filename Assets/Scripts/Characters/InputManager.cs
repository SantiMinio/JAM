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
                { InputName.ActionPlayerOne, KeyCode.Space },
                        { InputName.ActionPlayerTwo, KeyCode.Alpha0 }
    };

    static Dictionary<string, KeyCode> JoystickInputs = new Dictionary<string, KeyCode>()
    {
        { InputName.Jump, KeyCode.Joystick1Button0 },
                { InputName.ActionPlayerOne, KeyCode.Joystick1Button6 },
                        { InputName.ActionPlayerTwo, KeyCode.Joystick1Button7 }
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
    public const string ActionPlayerOne = "ActionPlayerOne";
    public const string ActionPlayerTwo = "ActionPlayerTwo";
}

[Serializable] public class UnityEvFloat : UnityEvent<float> { }

[Serializable] public class UnityEvKeyButton : UnityEvent<KeyEventButon> { }
