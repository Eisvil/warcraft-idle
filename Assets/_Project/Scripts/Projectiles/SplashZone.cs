using System;
using System.Collections.Generic;
using UnityEngine;

public class SplashZone : MonoBehaviour
{
    private bool _isEnemy;
    
    public List<IDamageable> Targets { get; private set; } = new List<IDamageable>();

    public void Init(bool isEnemy, float size)
    {
        _isEnemy = true;

        transform.localScale = Vector3.one * size;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (unit.IsEnemy != _isEnemy)
                Targets.Add(unit);
        }
        
        if(!_isEnemy) return;

        if (other.TryGetComponent(out Castle castle))
        {
            Targets.Add(castle);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (unit.IsEnemy != _isEnemy)
                Targets.Remove(unit);
        }
        
        if(!_isEnemy) return;

        if (other.TryGetComponent(out Castle castle))
        {
            Targets.Remove(castle);
        }
    }
}
