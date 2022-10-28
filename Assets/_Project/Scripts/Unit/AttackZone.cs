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
        
        target.IsDied += RemoveTarget;
        _targets.Add(target);
        _unit.StartAttacking();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out DamageableObject target)) return;

        if (!_targets.Contains(target)) return;
        
        RemoveTarget(target);
    }
    
    public void RemoveTarget(DamageableObject target)
    {
        if(!_targets.Contains(target)) return;
        
        target.IsDied -= RemoveTarget;
        _targets.Remove(target);
    }
}
