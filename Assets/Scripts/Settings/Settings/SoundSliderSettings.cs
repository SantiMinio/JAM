using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSliderSettings : SliderSettings
{
    [SerializeField] AudioMixer mixer = null;
    [SerializeField] string exposedVolumeParam = "Volume";
    [SerializeField] float initValue = 1.0f;

    protected override void Awake()
    {
        slider.value = initValue;
        base.Awake();
    }

    protected override void ChangeValue(float value)
    {
        var logValue = -80f;
        if (value > 0)
            logValue = Mathf.Log10(value) * 20;
        mixer.SetFloat(exposedVolumeParam, logValue);
    }
}
