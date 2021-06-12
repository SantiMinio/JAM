using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum KeyEventButon
{
    KeyDown = 0,
    Key = 1,
    KeyUp = 2
}

public class CharacterController : MonoBehaviour
{
    [SerializeField] UnityEvFloat AxisHorizontalOne;
    [SerializeField] UnityEvFloat AxisVerticalOne;
    [SerializeField] UnityEvFloat AxisHorizontalTwo;
    [SerializeField] UnityEvFloat AxisVerticalTwo;

    [SerializeField] UnityEvKeyButton JumpButton;
    [SerializeField] UnityEvKeyButton ActionCharOneButton;
    [SerializeField] UnityEvKeyButton ActionCharTwoButton;

    bool dead;

    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvents.CharactersSeparate, CharacterDead);
    }

    private void Update()
    {
        if (dead) return;
        float axisHorizontalOne = Input.GetAxis("HorizontalOne");
        float axisVerticalOne = Input.GetAxis("VerticalOne");
        float axisHorizontalTwo = Input.GetAxis("HorizontalTwo");
        float axisVerticalTwo = Input.GetAxis("VerticalTwo");

        AxisHorizontalOne?.Invoke(axisHorizontalOne);
        AxisVerticalOne?.Invoke(axisVerticalOne);
        AxisHorizontalTwo?.Invoke(axisHorizontalTwo);
        AxisVerticalTwo?.Invoke(axisVerticalTwo);

        if (InputManager.GetInput(InputName.Jump, KeyEventButon.KeyDown)) JumpButton?.Invoke(KeyEventButon.KeyDown);
        else if (InputManager.GetInput(InputName.Jump, KeyEventButon.Key)) JumpButton?.Invoke(KeyEventButon.Key);
        else if (InputManager.GetInput(InputName.Jump, KeyEventButon.KeyUp)) JumpButton?.Invoke(KeyEventButon.KeyUp);

        if (InputManager.GetInput(InputName.ActionPlayerOne, KeyEventButon.KeyDown)) ActionCharOneButton?.Invoke(KeyEventButon.KeyDown);
        else if (InputManager.GetInput(InputName.ActionPlayerOne, KeyEventButon.Key)) ActionCharOneButton?.Invoke(KeyEventButon.Key);
        else if (InputManager.GetInput(InputName.ActionPlayerOne, KeyEventButon.KeyUp)) ActionCharOneButton?.Invoke(KeyEventButon.KeyUp);
        else if (InputManager.GetInput(InputName.ActionPlayerOneAlt, KeyEventButon.KeyDown)) ActionCharOneButton?.Invoke(KeyEventButon.KeyDown);
        else if (InputManager.GetInput(InputName.ActionPlayerOneAlt, KeyEventButon.Key)) ActionCharOneButton?.Invoke(KeyEventButon.Key);
        else if (InputManager.GetInput(InputName.ActionPlayerOneAlt, KeyEventButon.KeyUp)) ActionCharOneButton?.Invoke(KeyEventButon.KeyUp);

        if (InputManager.GetInput(InputName.ActionPlayerTwo, KeyEventButon.KeyDown)) ActionCharTwoButton?.Invoke(KeyEventButon.KeyDown);
        else if (InputManager.GetInput(InputName.ActionPlayerTwo, KeyEventButon.Key)) ActionCharTwoButton?.Invoke(KeyEventButon.Key);
        else if (InputManager.GetInput(InputName.ActionPlayerTwo, KeyEventButon.KeyUp)) ActionCharTwoButton?.Invoke(KeyEventButon.KeyUp);
    }

    void CharacterDead()
    {
        AxisHorizontalOne?.Invoke(0);
        AxisVerticalOne?.Invoke(0);
        AxisHorizontalTwo?.Invoke(0);
        AxisVerticalTwo?.Invoke(0);
        dead = true;
    }

}