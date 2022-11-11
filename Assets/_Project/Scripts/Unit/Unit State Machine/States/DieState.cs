using UnityEngine;

public class DieState : State
{
    private void DisableUnit()
    {
        Unit.gameObject.SetActive(false);
    }
    
    public override void Enter()
    {
        base.Enter();
        
        Unit.Animator.SetInteger("Die_Id", Random.Range(0, 4));
        
        Unit.Animator.SetTrigger("Die");

        Unit.gameObject.layer = LayerMask.NameToLayer("Dead Unit");

        Unit.AnimationEvents.IsDying += DisableUnit;
    }

    public override void UpdateHandle() {}

    public override void FixedUpdateHandle() {}

    public override void Exit()
    {
        Unit.AnimationEvents.IsDying -= DisableUnit;
        
        base.Exit();
    }
}