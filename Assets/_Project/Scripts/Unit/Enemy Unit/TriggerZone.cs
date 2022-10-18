using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TriggerZone : MonoBehaviour
{
    private SphereCollider _collider;
    private EnemyUnit _unit;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public void Init(EnemyUnit unit, float attackRange)
    {
        _unit = unit;
        _collider.radius = attackRange * 5f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerUnit unit))
        {
            _unit.TrySetTarget(unit);
        }
    }
}
