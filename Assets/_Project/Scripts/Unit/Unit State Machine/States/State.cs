using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitStateMachine))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private UnitState _name;
    [SerializeField] private UnitState _exitState;
    
    protected UnitStateMachine UnitStateMachine;
    protected Unit Unit;
    
    public UnitState Name => _name;
    public bool IsEnabled { get; protected set; }

    public void Init(UnitStateMachine stateMachine, Unit unit)
    {
        UnitStateMachine = stateMachine;
        Unit = unit;
    }

    public virtual void Enter()
    {
        IsEnabled = true;
    }

    public abstract void UpdateHandle();

    public abstract void FixedUpdateHandle();

    public virtual void Exit()
    {
        IsEnabled = false;
        
        if(_exitState != UnitState.Empty)
            UnitStateMachine.SetState(_exitState);
    }
}