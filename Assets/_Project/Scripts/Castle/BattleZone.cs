using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleZone : MonoBehaviour
{
    public List<DamageableObject> Enemies { get; private set; } = new List<DamageableObject>();

    public event UnityAction<DamageableObject> IsEnemyEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out EnemyUnit enemy)) return;
        
        Enemies.Add(enemy);
        enemy.IsDied += RemoveEnemyFromList;
            
        IsEnemyEntered?.Invoke(enemy);
    }

    private void RemoveEnemyFromList(DamageableObject enemy)
    {
        Enemies.Remove(enemy);
        enemy.IsDied -= RemoveEnemyFromList;
    }
    
    public DamageableObject GetClosestEnemy(Vector3 unitPosition)
    {
        DamageableObject closestEnemy = null;
        var closestDistance = float.MaxValue;
        
        foreach (var enemy in Enemies)
        {
            var enemyDistance = Vector3.Distance(unitPosition, enemy.transform.position);

            if (!(enemyDistance < closestDistance)) continue;
            
            closestDistance = enemyDistance;
            closestEnemy = enemy;
        }

        return closestEnemy;
    }
}
