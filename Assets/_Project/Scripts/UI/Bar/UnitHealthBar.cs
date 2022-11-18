using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBar : Bar
{
    [SerializeField] private Unit _unit;

    private float MaxHealth => _unit.Stats.MaxHealth;
    private float CurrentHealth => _unit.Health;

    private void OnEnable()
    {
        Show(CurrentHealth, MaxHealth, false);
        _unit.IsHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _unit.IsHealthChanged -= OnHealthChanged;
    }

    private void Update()
    {
        transform.parent.LookAt(Camera.main.transform);
    }

    private void OnHealthChanged(bool isAnimationNeeded)
    {
        Show(CurrentHealth, MaxHealth, isAnimationNeeded);
    }
}
