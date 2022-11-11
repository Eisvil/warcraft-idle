using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private Castle _castle;
    [SerializeField] private PlayerUnitSpawner _playerUnitSpawner;
    [SerializeField] private EnemyUnitSpawner _enemyUnitSpawner;
    
    public Castle Castle => _castle;

    public void StartBattle()
    {
        _playerUnitSpawner.Init();
        _enemyUnitSpawner.Init();
        
        UIManager.Instance.GameplayScreen.Enable();
    }
}
