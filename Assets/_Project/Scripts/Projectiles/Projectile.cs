using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 50f;

    private Transform _target;
    private bool _isShot = false;

    protected Unit RootUnit;
    protected float Damage;
    
    public void Init(Unit rootUnit, float damage)
    {
        RootUnit = rootUnit;
        Damage = damage;

        if (rootUnit.IsEnemy)
        {
            _target = rootUnit.Target == null ? BattleManager.Instance.Castle.transform : rootUnit.Target.transform;
        }
        else
        {
            _target = rootUnit.Target.transform;
        }

        Shoot();
    }
    
    private void Update()
    {
        TryMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (unit.transform == _target)
            {
                DealDamage(unit);
            }
        }

        if (other.TryGetComponent(out Castle castle))
        {
            if (castle.transform == _target)
            {
                DealDamage(castle);
            }
        }
    }

    private void TryMove()
    {
        if(!_isShot) return;
        
        transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * _speed);
    }
    
    private void Shoot()
    {
        _isShot = true;
    }

    protected abstract void DealDamage(IDamageable target);
}
