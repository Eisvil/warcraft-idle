using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackZone : AttackZone
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Unit unit) || RootUnit.IsEnemy == unit.IsEnemy) return;
        
        Units.Add(unit);
        
        IsUnitEntered?.Invoke(unit);
    }
}
