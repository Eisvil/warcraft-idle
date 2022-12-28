using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Arrow : Projectile
{
    protected override void DealDamage(IDamageable target)
    {
        target.TakeDamage(Damage);
        
        Destroy(gameObject);
    }
}
