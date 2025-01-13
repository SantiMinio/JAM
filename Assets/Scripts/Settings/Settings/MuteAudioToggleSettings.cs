using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteAudioToggleSettings : ToggleSettings
{
    [SerializeField] AudioMixer mixer = null;
    [SerializeField] string[] exposedVolumeParams;
    [SerializeField] Slider[] sliders = new Slider[0];

    private void Start()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].onValueChanged.AddListener(new UnityEngine.Events.UnityAction<float>(OnChangeSliderValue));
        }
    }

    protected override void ChangeValue(bool value)
    {
        if (value)
        {
            for (int i = 0; i < exposedVolumeParams.Length; i++)
            {
                mixer.SetFloat(exposedVolumeParams[i], -80f);
            }
        }
        else
        {
            for (int i = 0; i < exposedVolumeParams.Length; i++)
            {
                sliders[i].value = sliders[i].value + (sliders[i].value == 1 ? -0.00001f : 0.00001f);
            }
        }
    }

    void OnChangeSliderValue(float value)
    {
        if (!toggle.isOn) return;

        toggle.isOn = false;
    }
}
