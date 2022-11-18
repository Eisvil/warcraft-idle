using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class Bar : MonoBehaviour
{
    private Slider _slider;
    private Tween _tween;

    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected void Show(float value, float maxValue, bool isAnimationNeeded = true)
    {
        _slider.maxValue = maxValue;
        
        if (isAnimationNeeded)
        {
            _tween = _slider.DOValue(value, 0.3f);
        }
        else
        {
            _slider.value = value;
        }
    }
}
