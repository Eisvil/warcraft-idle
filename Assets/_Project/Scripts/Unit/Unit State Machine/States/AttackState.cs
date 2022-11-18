using DG.Tweening;
using UnityEngine;

public class AttackState : State
{
    protected virtual void Attack()
    {
        var randomValue = Random.Range(0, 100);
        var finalDamage = Unit.Stats.Damage;

        if (Unit.Stats.CritChance > randomValue)
        {
            finalDamage *= Unit.Stats.CritMultiplier;
        }
        
        Unit.Target.TakeDamage(finalDamage);
    }

    public override void Enter()
    {
        base.Enter();

        Unit.AnimationEvents.IsAttacking += Attack;
        
        Unit.Animator.SetBool("Is_Attacking", true);
        Unit.Animator.speed = Unit.Stats.AttackSpeed;

        Unit.LookAtTarget();
    }

    public override void UpdateHandle() {}

    public override void FixedUpdateHandle() {}

    public override void Exit()
    {
        Unit.AnimationEvents.IsAttacking -= Attack;

        Unit.Animator.speed = 1f;
        Unit.Animator.SetBool("Is_Attacking", false);

        base.Exit();
    }
}
