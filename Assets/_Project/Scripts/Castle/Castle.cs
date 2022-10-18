using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Castle : DamageableObject
{
    [SerializeField] private PlayerUnitSpawner _playerUnitSpawner;

    public BattleZone BattleZone { get; private set; }

    private void Awake()
    {
        BattleZone = GetComponentInChildren<BattleZone>();
        Health = 100;
    }

    private void OnEnable()
    {
        BattleZone.IsEnemyEntered += TryTargetEnemy;
    }

    private void OnDisable()
    {
        BattleZone.IsEnemyEntered -= TryTargetEnemy;
    }

    private void TryTargetEnemy(DamageableObject enemy)
    {
        foreach (var unit in _playerUnitSpawner.GetActiveUnits())
        {
            unit.TrySetTarget(enemy);
        }
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        
        if(Health <= 0)
            Die();
    }
}
