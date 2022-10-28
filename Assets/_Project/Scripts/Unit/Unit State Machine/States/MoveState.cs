using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _speed;

    private void MoveToTarget()
    {
        if (Unit.Target == null)
        {
            Exit();
        }
        
        var targetPosition = Unit.Target.transform.position;
        var direction =  (targetPosition - Unit.transform.position).normalized;
        
        Unit.transform.LookAt(new Vector3(targetPosition.x, Unit.transform.position.y, targetPosition.z));
        
        Unit.Rigidbody.velocity = direction * _speed;
    }

    public override void Enter()
    {
        base.Enter();
        
        Unit.Animator.SetBool("Is_Moving", true);
    }

    public override void FixedUpdateHandle()
    {
        MoveToTarget();
    }

    public override void Exit()
    {
        Unit.Animator.SetBool("Is_Moving", false);
        
        Unit.Rigidbody.velocity = Vector3.zero;

        base.Exit();
    }
}
