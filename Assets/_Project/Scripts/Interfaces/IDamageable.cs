using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    void Die();
    void TakeDamage(float damage);
}
