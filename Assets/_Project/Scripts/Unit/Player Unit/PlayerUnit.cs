using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    private Castle Castle => BattleManager.Instance.Castle;

    protected override void RemoveTarget(DamageableObject unit)
    {
        base.RemoveTarget(unit);

        var closestEnemy = Castle.BattleZone.GetClosestEnemy(transform.position);
        
        if(closestEnemy == null) return;
        
        TrySetTarget(closestEnemy);
    }
}
