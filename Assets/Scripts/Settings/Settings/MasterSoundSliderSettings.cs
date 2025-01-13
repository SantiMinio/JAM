using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterSoundSliderSettings : SliderSettings
{
    [SerializeField] Slider[] sliders;

    protected override void Awake()
    {
        slider.value = 1;
        base.Awake();
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].onValueChanged.AddListener(new UnityEngine.Events.UnityAction<float>(ChangeValueInSlider));
        }
    }

    protected override void ChangeValue(float value)
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i].value > value)
            {
                sliders[i].value = value;
            }
        }
    }

    void ChangeValueInSlider(float value)
    {
        if(value > slider.value)
        {
            slider.SetValueWithoutNotify(value);
        }
    }
}
