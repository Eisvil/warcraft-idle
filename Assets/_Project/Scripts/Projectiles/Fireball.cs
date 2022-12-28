using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    [SerializeField] private SplashZone _splashZone;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _splashSize;
    
    protected override void DealDamage(IDamageable target)
    {
        _splashZone.Init(RootUnit.IsEnemy, _splashSize);

        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        
        target.TakeDamage(Damage);
        
        foreach (var splashTarget in _splashZone.Targets)
        {
            splashTarget.TakeDamage(Damage);
        }
        
        Destroy(gameObject);
    }
}
