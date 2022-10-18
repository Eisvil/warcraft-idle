using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class DamageableObject : MonoBehaviour
{
    [SerializeField] protected bool _isEnemy;
    
    protected float Health;

    public bool IsEnemy => _isEnemy;
    
    public event UnityAction<DamageableObject> IsDied;

    public virtual void TakeDamage(float damage)
    {
        
    }
    
    protected virtual void Die()
    {
        IsDied?.Invoke(this);
        
        gameObject.SetActive(false);
    }
}
