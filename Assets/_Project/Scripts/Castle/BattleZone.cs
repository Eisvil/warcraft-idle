using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BattleZone : TriggerZone
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Unit unit) || !unit.IsEnemy) return;
        
        Units.Add(unit);
        unit.IsDying += RemoveUnit;

        IsUnitEntered?.Invoke(unit);
    }

    public override void RemoveUnit(Unit unit)
    {
        base.RemoveUnit(unit);
        
        unit.IsDying -= RemoveUnit;
    }
}
