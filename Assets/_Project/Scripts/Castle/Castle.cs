using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Castle : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerUnitSpawner _playerUnitSpawner;
    [SerializeField] private BattleZone _battleZone;
    
    private float _health;

    public BattleZone BattleZone => _battleZone;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _health = 100;
    }

    public void Die()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        
        if(_health <= 0)
            Die();
    }
}
