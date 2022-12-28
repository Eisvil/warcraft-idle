using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RangeAttackState : AttackState
{
    [SerializeField] private Projectile _projectileTemplate;
    [SerializeField] private Transform _projecSpawnPoint;

    protected override void Attack()
    {
        var randomValue = Random.Range(0, 100);
        var finalDamage = Unit.Stats.Damage;
        
        if (Unit.Stats.CritChance > randomValue)
        {
            finalDamage *= Unit.Stats.CritMultiplier;
        }

        var projectile = Instantiate(_projectileTemplate, _projecSpawnPoint.position, _projecSpawnPoint.rotation);
        
        projectile.Init(Unit, finalDamage);
    }
}
