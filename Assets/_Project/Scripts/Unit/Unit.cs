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
    
    private UnitStats _baseStats;
    
    protected AttackZone AttackZone;
    protected UnitStateMachine StateMachine;
    protected Tween Tween;
    protected PerkManager PerkManager => PerkManager.Instance;
    
    public int Id { get; private set; }
    public float Health { get; protected set; }
    public bool IsEnemy { get; private set; }
    public UnityAction<bool> IsHealthChanged;

    public UnitStats Stats
    { 
        get
        {
            var stats = new UnitStats();
            
            if (!IsEnemy)
            {
                stats.MaxHealth = _baseStats.MaxHealth *
                                  (1 + PerkManager.GetPerkLevel(PerkName.UnitHealth) * PerkManager.PerkMultiplier);
                stats.Damage = _baseStats.Damage *
                               (1 + PerkManager.GetPerkLevel(PerkName.Damage) * PerkManager.PerkMultiplier);
                stats.CritChance = _baseStats.CritChance *
                                   (1 + PerkManager.GetPerkLevel(PerkName.CritChance) * PerkManager.PerkMultiplier);
                stats.CritMultiplier = _baseStats.CritMultiplier *
                                       (1 + PerkManager.GetPerkLevel(PerkName.CritMultiplier) * PerkManager.PerkMultiplier);
                stats.AttackSpeed = _baseStats.AttackSpeed *
                                    (1 + PerkManager.GetPerkLevel(PerkName.AttackSpeed) * PerkManager.PerkMultiplier);
                stats.MoveSpeed = _baseStats.MoveSpeed *
                                  (1 + PerkManager.GetPerkLevel(PerkName.MoveSpeed) * PerkManager.PerkMultiplier);
                stats.AttackRange = _baseStats.AttackRange;
            }
            else
            {
                stats.MaxHealth = _baseStats.MaxHealth *
                                  (1 + LevelManager.Instance.CurrentWave.UnitPerksLevel * PerkManager.PerkMultiplier);
                stats.Damage = _baseStats.Damage *
                               (1 + LevelManager.Instance.CurrentWave.UnitPerksLevel * PerkManager.PerkMultiplier);
                stats.CritChance = _baseStats.CritChance *
                                   (1 + LevelManager.Instance.CurrentWave.UnitPerksLevel * PerkManager.PerkMultiplier);
                stats.CritMultiplier = _baseStats.CritMultiplier *
                                       (1 + LevelManager.Instance.CurrentWave.UnitPerksLevel * PerkManager.PerkMultiplier);
                stats.AttackSpeed = _baseStats.AttackSpeed *
                                    (1 + LevelManager.Instance.CurrentWave.UnitPerksLevel * PerkManager.PerkMultiplier);
                stats.MoveSpeed = _baseStats.MoveSpeed *
                                  (1 + LevelManager.Instance.CurrentWave.UnitPerksLevel * PerkManager.PerkMultiplier);
                stats.AttackRange = _baseStats.AttackRange;
            }

            return stats;
        }
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
        Health = Stats.MaxHealth;
        
        IsHealthChanged?.Invoke(false);

        AttackZone.Init(Stats.AttackRange + 1f);
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
        
        IsHealthChanged?.Invoke(true);

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