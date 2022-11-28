using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSlider : SettingsSlider
{
    protected override void Init()
    {
        Slider.value = DataManager.Instance.Data.SoundVolume;
        
        SoundManager.Instance.ChangeSoundsVolume(Slider.value / Slider.maxValue);
        
        UpdateVisual();
    }

    protected override void OnValueChanged(float value)
    {
        DataManager.Instance.Data.SoundVolume = Convert.ToInt32(value);
        
        SoundManager.Instance.ChangeSoundsVolume(value / Slider.maxValue);
        
        UpdateVisual();
    }
}
