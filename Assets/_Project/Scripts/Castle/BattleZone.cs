using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleZone : MonoBehaviour
{
    private Castle _castle;
    
    public List<DamageableObject> Enemies { get; private set; } = new List<DamageableObject>();
    public event UnityAction<DamageableObject> IsEnemyEntered;

    public void Init(Castle castle)
    {
        _castle = castle;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out EnemyUnit enemy)) return;
        
        Enemies.Add(enemy);
        enemy.IsDied += OnEnemyDied;
            
        IsEnemyEntered?.Invoke(enemy);
    }

    private void OnEnemyDied(DamageableObject enemy)
    {
        Enemies.Remove(enemy);
        enemy.IsDied -= OnEnemyDied;
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
