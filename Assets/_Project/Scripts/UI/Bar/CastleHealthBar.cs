using System;
using TMPro;
using UnityEngine;

public class CastleHealthBar : Bar
{
    [SerializeField] private Castle _castle;
    [SerializeField] private TMP_Text _text;

    private float MaxHealth => _castle.MaxHealth;
    private float CurrentHealth => _castle.CurrentHealth;

    private void OnEnable()
    {
        _castle.IsHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _castle.IsHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(bool isAnimationNeeded)
    {
        Show(CurrentHealth, MaxHealth, isAnimationNeeded);

        _text.text = $"{Math.Round(CurrentHealth, 2)}/{MaxHealth}";
    }
}
