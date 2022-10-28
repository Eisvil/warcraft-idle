using UnityEngine;

public class DieState : State
{
    public override void Enter()
    {
        base.Enter();
        
        Unit.Animator.SetInteger("Die_Id", Random.Range(0, 4));
        
        Unit.Animator.SetTrigger("Die");
    }
}