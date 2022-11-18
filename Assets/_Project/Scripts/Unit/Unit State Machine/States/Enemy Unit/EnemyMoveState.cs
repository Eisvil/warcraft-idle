using DG.Tweening;
using UnityEngine;

public class EnemyMoveState : MoveState
{
    private bool _isTargetThere;
    private Castle Castle => BattleManager.Instance.Castle;
    
    protected override void MoveToTarget()
    {
        Vector3 targetPosition;
        Vector3 direction;
        
        if (_isTargetThere)
        {
            targetPosition = Unit.Target.transform.position;
            direction =  (targetPosition - Unit.transform.position).normalized;
        }
        else
        {
            targetPosition = Castle.transform.position;
            direction =  (targetPosition - Unit.transform.position).normalized;
        }

        Unit.Rigidbody.velocity = direction * (_speed * Unit.Stats.MoveSpeed);
    }

    public override void Enter()
    {
        base.Enter();
        
        _isTargetThere = Unit.Target != null;
    }
}