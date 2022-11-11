using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] protected float _speed;

    protected virtual void MoveToTarget()
    {
        var targetPosition = Unit.Target.transform.position;
        var direction =  (targetPosition - Unit.transform.position).normalized;

        Unit.Rigidbody.velocity = direction * _speed;
    }

    public override void Enter()
    {
        base.Enter();

        Unit.Animator.SetBool("Is_Moving", true);

        Unit.LookAtTarget();
    }

    public override void UpdateHandle() {}

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
