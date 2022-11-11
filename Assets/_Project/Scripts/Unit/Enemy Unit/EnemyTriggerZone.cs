using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class EnemyTriggerZone : TriggerZone
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Unit unit) || unit.IsEnemy) return;
        
        Units.Add(unit);

        IsUnitEntered?.Invoke(unit);
    }
}
