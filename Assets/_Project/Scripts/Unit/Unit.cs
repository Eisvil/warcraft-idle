using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(UnitAnimationEvents), typeof(Animator))]
public abstract class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private UnitRace _race;

    protected float Health;
    protected AttackZone AttackZone;
    protected UnitStateMachine StateMachine;
    protected Tween Tween;
    
    public int Id { get; private set; }
    public bool IsEnemy { get; private set; }
    public UnitStats Stats { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Unit Target { get; protected set; }
    public UnitAnimationEvents AnimationEvents { get; private set; }
    public UnitRace Race => _race;
    public event UnityAction<Unit> IsDying;

    protected void Awake()
    {
        StateMachine = GetComponentInChildren<UnitStateMachine>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        AttackZone = GetComponentInChildren<AttackZone>();
        AnimationEvents = GetComponent<UnitAnimationEvents>();
    }

    public virtual void Init(int id, UnitStats stats, bool isEnemy)
    {
        Id = id;
        Stats = stats;
        Health = Stats.MaxHealth;
        IsEnemy = isEnemy;
        
        AttackZone.Init(Stats.AttackRange + 0.5f);
        AttackZone.SetRootUnit(this);
    }
    
    protected abstract void RemoveTarget(Unit unit);
    
    protected abstract void TrySetTarget(Unit unit);

    protected abstract void Attack(Unit unit);

    public abstract void TryFindTarget();

    public abstract void LookAtTarget();

    public abstract void Reset();

    public void TakeDamage(float damage)
    {
        if(StateMachine.CurrentState == UnitState.Die) return;
        
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        IsDying?.Invoke(this);
        
        Tween.Kill();
        
        StateMachine.SetState(UnitState.Die);
    }
}

[Serializable]
public class UnitStats
{
    public float MaxHealth;    
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