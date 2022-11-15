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

    private UnitStats _currentStats;
    private UnitStats _baseStats;
    
    protected float Health;
    protected AttackZone AttackZone;
    protected UnitStateMachine StateMachine;
    protected Tween Tween;
    protected PerkManager PerkManager => PerkManager.Instance;
    
    public int Id { get; private set; }
    public bool IsEnemy { get; private set; }

    public UnitStats Stats
    { 
        get
        {
            if (IsEnemy) return _currentStats;
            
            var stats = _currentStats;
            
            stats.MaxHealth += _baseStats.MaxHealth *  PerkManager.GetTemporaryPerkLevel(PerkName.UnitHealth);
            stats.Damage += _baseStats.Damage *  PerkManager.GetTemporaryPerkLevel(PerkName.Damage);
            stats.CritChance += 0.1f * PerkManager.GetTemporaryPerkLevel(PerkName.CritChance);
            stats.CritMultiplier += 0.1f * PerkManager.GetTemporaryPerkLevel(PerkName.CritMultiplier);
            stats.AttackSpeed += 0.1f * PerkManager.GetTemporaryPerkLevel(PerkName.AttackSpeed);
            stats.MoveSpeed += 0.1f * PerkManager.GetTemporaryPerkLevel(PerkName.MoveSpeed);

            return stats;
        }

        private set => _currentStats = value;
    }
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

    public virtual void Init(int id, UnitStats basicStats, bool isEnemy)
    {
        Id = id;
        IsEnemy = isEnemy;
        _baseStats = basicStats;

        if(isEnemy == false)
        {
            Stats = new UnitStats
            {
                MaxHealth = _baseStats.MaxHealth + _baseStats.MaxHealth * PerkManager.GetPermanentPerkLevel(PerkName.UnitHealth),
                Damage = _baseStats.Damage + _baseStats.Damage * PerkManager.GetPermanentPerkLevel(PerkName.Damage),
                CritChance = _baseStats.CritChance + 0.1f * PerkManager.GetPermanentPerkLevel(PerkName.CritChance),
                CritMultiplier = _baseStats.CritMultiplier +
                                 0.1f * PerkManager.GetPermanentPerkLevel(PerkName.CritMultiplier),
                AttackSpeed = _baseStats.AttackSpeed + 0.1f * PerkManager.GetPermanentPerkLevel(PerkName.AttackSpeed),
                AttackRange = _baseStats.AttackRange,
                MoveSpeed = _baseStats.MoveSpeed + 0.1f * PerkManager.GetPermanentPerkLevel(PerkName.MoveSpeed),
            };
            
            Health = Stats.MaxHealth;
        }
        else
        {
            Stats = _baseStats;
            Health = Stats.MaxHealth;
        }

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