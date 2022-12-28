using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Arrow : MonoBehaviour
{
    [FormerlySerializedAs("_flyDuration")] [SerializeField] private float _speed = 0.3f;
    
    private Tween _tween;
    private Unit _unit;
    private Transform _target;
    private float _damage;
    private bool _isShot = false;

    public void Init(Unit unit, float damage)
    {
        _unit = unit;
        _damage = damage;

        if (unit.IsEnemy)
        {
            _target = unit.Target == null ? BattleManager.Instance.Castle.transform : _unit.Target.transform;
        }
        else
        {
            _target = _unit.Target.transform;
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
                unit.TakeDamage(_damage);
                
                Destroy();
            }
        }

        if (other.TryGetComponent(out Castle castle))
        {
            if (castle.transform == _target)
            {
                castle.TakeDamage(_damage);
                
                Destroy();
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

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
