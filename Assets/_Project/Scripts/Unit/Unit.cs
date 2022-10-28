using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Unit : DamageableObject
{
    [SerializeField] private UnitRace _race;
    
    protected AttackZone AttackZone;
    
    public Rigidbody Rigidbody { get; private set; }
    public UnitStateMachine StateMachine { get; private set; }
    public Animator Animator { get; private set; }
    public DamageableObject Target { get; protected set; }
    public int Id { get; private set; }
    public UnitStats Stats { get; protected set; }
    public UnitRace Race => _race;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        StateMachine = GetComponentInChildren<UnitStateMachine>();
        Animator = GetComponent<Animator>();
        AttackZone = GetComponentInChildren<AttackZone>();
    }

    public virtual void Init(int id, UnitStats stats, bool isEnemy)
    {
        Id = id;
        Stats = stats;
        Health = Stats.Health;
        _isEnemy = isEnemy;
        
        AttackZone.Init(this, Stats.AttackRange);
        
        TryFindNextTarget();
    }

    protected virtual void RemoveTarget(DamageableObject target)
    {
        target.IsDied -= RemoveTarget;

        Target = null;
        
        AttackZone.RemoveTarget(target);
        
        TryFindNextTarget();
    }
    
    public override void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
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

    public virtual void TryFindNextTarget()
    {
        if(Target != null) return;
        
        if (AttackZone.Targets.Count >= 1)
        {
            TrySetTarget(AttackZone.Targets.First());
        }
        else
        {
            StateMachine.SetState(UnitState.Idle);
        }
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