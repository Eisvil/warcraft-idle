using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUnit : Unit
{
    private Castle Castle => BattleManager.Instance.Castle;

    public override void TryFindNextTarget()
    {
        if (AttackZone.Targets.Count >= 1)
        {
            TrySetTarget(AttackZone.Targets.First());
        }
        else
        {
            var target = Castle.BattleZone.GetClosestEnemy(transform.position);

            if (target != null)
            {
                TrySetTarget(target);
            }
            else
            {
                StateMachine.SetState(UnitState.Idle);
            }
        }
    }
}
