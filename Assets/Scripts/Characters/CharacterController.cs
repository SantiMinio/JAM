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

    private void Update()
    {
        float axisHorizontalOne = Input.GetAxis("HorizontalOne");
        float axisVerticalOne = Input.GetAxis("VerticalOne");
        float axisHorizontalTwo = Input.GetAxis("HorizontalTwo");
        float axisVerticalTwo = Input.GetAxis("VerticalTwo");

        AxisHorizontalOne?.Invoke(axisHorizontalOne);
        AxisVerticalOne?.Invoke(axisVerticalOne);
        AxisHorizontalTwo?.Invoke(axisHorizontalTwo);
        AxisVerticalTwo?.Invoke(axisVerticalTwo);
    }
}