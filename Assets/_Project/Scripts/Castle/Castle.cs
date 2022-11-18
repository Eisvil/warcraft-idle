using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour, IDamageable
{
    [SerializeField] private BattleZone _battleZone;

    private float _maxHealth;
    private float _currentHealth;
    private float _regenPerSecond;
    private float _timer;
    private bool _isMaxHealth;

    public BattleZone BattleZone => _battleZone;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public event UnityAction<bool> IsHealthChanged; 

    public void Init()
    {
        _maxHealth = 10 * (1 + PerkManager.Instance.GetPerkLevel(PerkName.CastleHealth) *
            PerkManager.Instance.PerkMultiplier * 20);
        _regenPerSecond = 0.5f * (1 + PerkManager.Instance.GetPerkLevel(PerkName.HealthRegen) *
            PerkManager.Instance.PerkMultiplier * 8);
        _currentHealth = _maxHealth;
        
        IsHealthChanged?.Invoke(false);

        PerkManager.Instance.IsAnyPerkUpgraded += OnPerkUpdated;
    }

    private void OnDisable()
    {
        PerkManager.Instance.IsAnyPerkUpgraded -= OnPerkUpdated;
    }

    private void Update()
    {
        Regen();
    }

    private void Regen()
    {
        if(_isMaxHealth) return;
        
        if (_currentHealth >= _maxHealth)
        {
            _isMaxHealth = true;
            _currentHealth = _maxHealth;
            
            IsHealthChanged?.Invoke(true);
        }

        _timer += Time.deltaTime;

        if (_timer < 1f) return;
        
        _currentHealth += _regenPerSecond;
        _timer = 0f;
        
        IsHealthChanged?.Invoke(true);
    }
    
    private void OnPerkUpdated(PerkName perkName)
    {
        switch(perkName)
        {
            case PerkName.CastleHealth:
                var currentHealthNormalized = _currentHealth / _maxHealth;
                
                _maxHealth = 10 * (1 + PerkManager.Instance.GetPerkLevel(PerkName.CastleHealth) *
                    PerkManager.Instance.PerkMultiplier * 20);

                _currentHealth = _maxHealth * currentHealthNormalized;
                
                IsHealthChanged?.Invoke(false);
                break;
            case PerkName.HealthRegen:
                _regenPerSecond = 0.5f * (1 + PerkManager.Instance.GetPerkLevel(PerkName.HealthRegen) *
                    PerkManager.Instance.PerkMultiplier * 8);
                break;
        }
    }
    
    public void Die()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        _isMaxHealth = false;
        
        IsHealthChanged?.Invoke(true);
        
        if(_currentHealth <= 0)
            Die();
    }
}
