using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackZone : MonoBehaviour
{
    private Unit _unit;
    private SphereCollider _collider;
    private List<DamageableObject> _targets = new List<DamageableObject>();

    public List<DamageableObject> Targets => _targets;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public void Init(Unit unit, float attackRange)
    {
        _unit = unit;
        _collider.radius = 0.5f + attackRange;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out DamageableObject target)) return;

        if (target.IsEnemy == _unit.IsEnemy) return;
        
        target.IsDied += OnTargetDied;
        _targets.Add(target);
        _unit.StartAttacking();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Unit unit)) return;

        if (unit.IsEnemy == _unit.IsEnemy) return;
        
        unit.IsDied -= OnTargetDied;
        _targets.Remove(unit);
    }
    
    private void OnTargetDied(DamageableObject target)
    {
        target.IsDied -= OnTargetDied;
        _targets.Remove(target);
    }
}
