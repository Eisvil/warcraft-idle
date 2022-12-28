using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : AttackState
{
    [SerializeField] private Arrow _arrowTemplate;
    [SerializeField] private Transform _arrowSpawnPoint;

    protected override void Attack()
    {
        var randomValue = Random.Range(0, 100);
        var finalDamage = Unit.Stats.Damage;
        
        if (Unit.Stats.CritChance > randomValue)
        {
            finalDamage *= Unit.Stats.CritMultiplier;
        }

        var arrow = Instantiate(_arrowTemplate, _arrowSpawnPoint.position, _arrowSpawnPoint.rotation);
        
        arrow.Init(Unit, finalDamage);
    }
}
