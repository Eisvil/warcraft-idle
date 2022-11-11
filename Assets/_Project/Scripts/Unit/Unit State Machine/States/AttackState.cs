using DG.Tweening;
using UnityEngine;

public class AttackState : State
{
    protected virtual void Attack()
    {
        Unit.Target.TakeDamage(Unit.Stats.Damage);
    }

    public override void Enter()
    {
        base.Enter();

        Unit.AnimationEvents.IsAttacking += Attack;
        
        Unit.Animator.SetBool("Is_Attacking", true);

        Unit.LookAtTarget();
    }

    public override void UpdateHandle() {}

    public override void FixedUpdateHandle() {}

    public override void Exit()
    {
        Unit.AnimationEvents.IsAttacking -= Attack;
        
        Unit.Animator.SetBool("Is_Attacking", false);

        base.Exit();
    }
}
