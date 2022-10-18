using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Unit : DamageableObject
{
    [SerializeField] private UnitRace _race;
    
    private AttackZone _attackZone;
    
    public UnitStateMachine StateMachine { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public DamageableObject Target { get; private set; }
    public int Id { get; private set; }
    public UnitStats Stats { get; protected set; }
    public UnitRace Race => _race;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        StateMachine = GetComponentInChildren<UnitStateMachine>();
        _attackZone = GetComponentInChildren<AttackZone>();
    }

    public virtual void Init(int id, UnitStats stats, bool isEnemy)
    {
        Id = id;
        Stats = stats;
        Health = Stats.Health;
        _isEnemy = isEnemy;
        
        _attackZone.Init(this, Stats.AttackRange);
    }

    protected virtual void RemoveTarget(DamageableObject unit)
    {
        unit.IsDied -= RemoveTarget;

        Target = null;
            
        StateMachine.SetState(UnitState.Idle);
    }
    
    public override void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        Target.TakeDamage(Stats.Damage);
    }

    public void TrySetTarget(DamageableObject target)
    {
        if(Target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) >=
                Vector3.Distance(transform.position, Target.transform.position)) return;
            
            Target.IsDied -= RemoveTarget;
        }
        
        Target = target;

        Target.IsDied += RemoveTarget;
        
        StateMachine.SetState(UnitState.Move);
    }

    public void StartAttacking()
    {
        StateMachine.SetState(UnitState.Attack);
    }
    
    public virtual void Reset()
    {
        Health = Stats.Health;
        Target = null;
        StateMachine.Reset();
    }
}

[Serializable]
public class UnitStats
{
    public float Health;    
    public float Damage;
    public float CritChance;
    public float CritMultiplier;
    public float AttackSpeed;
    public float AttackRange;
    public float MoveSpeed;
}

[Serializable]
public enum UnitRace
{
    Human,
    Orc,
    Elf,
    Undead
}