using UnityEngine;

public class AttackState : State
{
    private float _timer;

    private void Attack()
    {
        if (Unit.Target == null)
        {
            Exit();
        }
        
        Unit.Target.TakeDamage(Unit.Stats.Damage);
    }
    
    public override void Enter()
    {
        base.Enter();

        _timer = 0;
        
        Unit.Animator.SetBool("Is_Attacking", true);
    }

    public override void UpdateHandle()
    {
        _timer += Time.deltaTime;

        if (!(_timer >= 1 / Unit.Stats.AttackSpeed)) return;
        
        Attack();
        _timer = 0f;
    }

    public override void Exit()
    {
        Unit.Animator.SetBool("Is_Attacking", false);
        
        base.Exit();
    }
}
