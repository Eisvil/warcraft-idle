using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SettingsSlider : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentValue;
    [SerializeField] private TMP_Text _maxValue;
    [SerializeField] private Image _icon;
    [SerializeField] protected Slider Slider;
    [Header("References")]
    [SerializeField] private Sprite _enabledIcon;
    [SerializeField] private Sprite _disabledIcon;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    protected abstract void Init();

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        Slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    protected void UpdateVisual()
    {
        _currentValue.text = Slider.value.ToString();
        _currentValue.color = Slider.value > 0 ? _activeColor : _inactiveColor;
        _maxValue.color = Slider.value == Slider.maxValue ? _activeColor : _inactiveColor;
        _icon.sprite = Slider.value > 0 ? _enabledIcon : _disabledIcon;
        _icon.SetNativeSize();
    }
    
    protected abstract void OnValueChanged(float value);
}
