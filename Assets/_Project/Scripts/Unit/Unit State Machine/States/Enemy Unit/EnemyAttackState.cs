using UnityEngine;

public class EnemyAttackState : AttackState
{
    private bool _isTargetThere;
    private Castle Castle => BattleManager.Instance.Castle;

    protected override void Attack()
    {
        if(_isTargetThere)
        {
            Unit.Target.TakeDamage(Unit.Stats.Damage);
        }
        else
        {
            Castle.TakeDamage(Unit.Stats.Damage);
        }
    }
    
    public override void Enter()
    {
        base.Enter();

        _isTargetThere = Unit.Target != null;
    }
}
