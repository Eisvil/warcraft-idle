using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour
{
    [SerializeField] private UnitState _startState;
    
    private State _currentState;
    private List<State> _states;

    public Unit Unit { get; private set; }
    public UnitState CurrentState => _currentState.Name;

    private void Awake()
    {
        Unit = GetComponentInParent<Unit>();

        _states = GetComponents<State>().ToList();

        foreach (var state in _states)
        {
            state.Init(this, Unit);
        }
    }

    /*private void Start()
    {
        Reset();
    }*/

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateHandle();
        }
    }

    private void FixedUpdate()
    {
        if (_currentState != null)
        {
            _currentState.FixedUpdateHandle();
        }
    }

    public void Reset()
    {
        SetState(_startState);
    }

    public void SetState(UnitState stateName)
    {
        if (_currentState != null)
        {
            if(_currentState.IsEnabled)
                _currentState.Exit();
        }

        _currentState = _states.FirstOrDefault(state => state.Name == stateName);
        
        if(_currentState != null)
            _currentState.Enter();
    }
}

[Serializable]
public enum UnitState
{
    Idle,
    Move,
    Attack,
    Die,
    Empty
}
