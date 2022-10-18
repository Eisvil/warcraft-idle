using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _speed;

    private void MoveToTarget()
    {
        var direction =  (Unit.Target.transform.position - Unit.transform.position).normalized;
        
        Unit.Rigidbody.velocity = direction * _speed;
    }

    public override void FixedUpdateHandle()
    {
        MoveToTarget();
    }

    public override void Exit()
    {
        Unit.Rigidbody.velocity = Vector3.zero;
        
        base.Exit();
    }
}
