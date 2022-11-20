using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyUnit : Unit
{
    [SerializeField] private EnemyTriggerZone _enemyTriggerZone;

    public override void Init(int id, UnitStats basicStats, bool isEnemy)
    {
        base.Init(id, basicStats, isEnemy);
        
        _enemyTriggerZone.Init(basicStats.AttackRange * 5f);
    }

    private void OnEnable()
    {
        _enemyTriggerZone.IsUnitEntered += TrySetTarget;
        AttackZone.IsUnitEntered += Attack;
    }

    private void OnDisable()
    {
        _enemyTriggerZone.IsUnitEntered -= TrySetTarget;
        AttackZone.IsUnitEntered -= Attack;
    }

    protected override void RemoveTarget(Unit unit)
    {
        Target.IsDying -= RemoveTarget;
        Target = null;
        
        _enemyTriggerZone.RemoveUnit(unit);
        AttackZone.RemoveUnit(unit);

        TryFindTarget();
    }

    protected override void TrySetTarget(Unit unit)
    {
        if(Target != null)
        {
            var distanceToNewTarget = Vector3.Distance(transform.position, unit.transform.position);
            var distanceToCurrentTarget = Vector3.Distance(transform.position, Target.transform.position);

            if (distanceToNewTarget >= distanceToCurrentTarget) return;
                
            Target.IsDying -= RemoveTarget;
        }

        Target = unit;
        Target.IsDying += RemoveTarget;
        
        StateMachine.SetState(UnitState.Move);
    }

    protected override void Attack(Unit unit)
    {
        if(Target != null && StateMachine.CurrentState == UnitState.Attack) return;
        
        if(Target != null)
            Target.IsDying -= RemoveTarget;
        
        Target = unit;
        
        if(Target != null)
            Target.IsDying += RemoveTarget;
    
        StateMachine.SetState(UnitState.Attack);
    }

    public override void TryFindTarget()
    {
        var newTarget = AttackZone.GetUnitInZone();

        if (newTarget != null)
        {
            Attack(newTarget);
        }
        else
        {
            newTarget = _enemyTriggerZone.GetClosestEnemy(transform.position);
            
            if (newTarget != null)
            {
                TrySetTarget(newTarget);
            }
            else
            {
                StateMachine.SetState(UnitState.Move);
            }
        }
    }

    public override void LookAtTarget()
    {
        if(StateMachine.CurrentState == UnitState.Die) return;
        
        transform.DOLookAt(Target != null ? Target.transform.position : BattleManager.Instance.Castle.transform.position, 0.35f).SetLink(gameObject);
        
        Tween.OnComplete(LookAtTarget);
    }

    public override void Reset()
    {
        Health = Stats.MaxHealth;
        
        IsHealthChanged?.Invoke(false);
        
        StateMachine.Reset();
        AttackZone.Clear();
        _enemyTriggerZone.Clear();
        
        gameObject.layer = LayerMask.NameToLayer("Unit");
        
        if (Target == null) return;
    
        Target.IsDying -= RemoveTarget;
        Target = null;
    }
}
