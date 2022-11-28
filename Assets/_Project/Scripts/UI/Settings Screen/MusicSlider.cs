using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSlider : SettingsSlider
{
    protected override void Init()
    {
        Slider.value = DataManager.Instance.Data.MusicVolume;
        
        SoundManager.Instance.ChangeSoundsVolume(Slider.value / Slider.maxValue);
        
        UpdateVisual();
    }

    protected override void OnValueChanged(float value)
    {
        DataManager.Instance.Data.MusicVolume = Convert.ToInt32(value);
        
        SoundManager.Instance.ChangeMusicVolume(value / Slider.maxValue);
        
        UpdateVisual();
    }
}
