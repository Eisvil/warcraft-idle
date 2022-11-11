using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackZone : AttackZone
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) && RootUnit.IsEnemy != unit.IsEnemy)
        {
            Units.Add(unit);

            IsUnitEntered?.Invoke(unit);
        }

        if (other.TryGetComponent(out Castle castle))
        {
            Debug.Log("Castle collision");
            
            IsUnitEntered?.Invoke(null);
        }
    }
}
