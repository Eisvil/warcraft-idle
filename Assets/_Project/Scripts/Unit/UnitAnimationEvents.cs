using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitAnimationEvents : MonoBehaviour
{
    public event UnityAction IsAttacking;
    public event UnityAction IsDying;

    public void InvokeAttackEvent()
    {
        IsAttacking?.Invoke();
    }
    
    public void InvokeDieEvent()
    {
        IsDying?.Invoke();
    }
}
