using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    private TriggerZone _triggerZone;

    protected override void Awake()
    {
        base.Awake();

        _triggerZone = GetComponentInChildren<TriggerZone>();
    }

    public override void Init(int id, UnitStats stats, bool isEnemy)
    {
        base.Init(id, stats, isEnemy);

        _triggerZone.Init(this, stats.AttackRange);
    }
}
