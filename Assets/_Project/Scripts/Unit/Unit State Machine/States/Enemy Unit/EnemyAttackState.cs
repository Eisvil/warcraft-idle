using UnityEngine;

public class EnemyAttackState : AttackState
{
    private bool _isTargetThere;
    private Castle Castle => BattleManager.Instance.Castle;

    protected override void Attack()
    {
        var randomValue = Random.Range(0, 100);
        var finalDamage = Unit.Stats.Damage;

        if (Unit.Stats.CritChance > randomValue)
        {
            finalDamage *= Unit.Stats.CritMultiplier;
        }
        
        if(_isTargetThere)
        {
            Unit.Target.TakeDamage(finalDamage);
        }
        else
        {
            Castle.TakeDamage(finalDamage);
        }
        
        SoundManager.Instance.PlaySound(SoundName.SwordHit);
    }
    
    public override void Enter()
    {
        base.Enter();

        _isTargetThere = Unit.Target != null;
    }
}
