using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerUnit : Unit
{
    private Castle Castle => BattleManager.Instance.Castle;

    private void OnEnable()
    {
        Castle.BattleZone.IsUnitEntered += TrySetTarget;
        AttackZone.IsUnitEntered += Attack;
    }

    private void OnDisable()
    {
        Castle.BattleZone.IsUnitEntered -= TrySetTarget;
        AttackZone.IsUnitEntered -= Attack;
    }

    protected override void RemoveTarget(Unit unit)
    {
        Target.IsDying -= RemoveTarget;
        Target = null;
        
        Castle.BattleZone.RemoveUnit(unit);
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
        if(Target == unit && StateMachine.CurrentState == UnitState.Attack) return;
        
        if(Target != null)
            Target.IsDying -= RemoveTarget;

        Target = unit;
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
            newTarget = Castle.BattleZone.GetClosestEnemy(transform.position);
            
            if (newTarget != null)
            {
                TrySetTarget(newTarget);
            }
            else
            {
                StateMachine.SetState(UnitState.Idle);
            }
        }
    }

    public override void LookAtTarget()
    {
        if (Target == null || StateMachine.CurrentState == UnitState.Die) return;
            
        Tween = transform.DOLookAt(Target.transform.position, 0.35f);

        Tween.OnComplete(LookAtTarget);
    }

    public override void Reset()
    {
        Health = Stats.MaxHealth;
        StateMachine.Reset();
        AttackZone.Clear();

        gameObject.layer = LayerMask.NameToLayer("Unit");
        
        if (Target == null) return;
    
        Target.IsDying -= RemoveTarget;
        Target = null;
    }
}
