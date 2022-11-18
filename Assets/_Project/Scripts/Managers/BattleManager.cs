using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private Castle _castle;
    [SerializeField] private PlayerUnitSpawner _playerUnitSpawner;
    [SerializeField] private EnemyUnitSpawner _enemyUnitSpawner;
    
    public Castle Castle => _castle;
    public bool IsBattleGoing { get; private set; }

    private void OnDisable()
    {
        LevelManager.Instance.IsWaveCompleted -= OnWaveCompleted;
        LevelManager.Instance.IsLevelCompleted -= OnLevelCompleted;
    }

    private void Start()
    {
        LevelManager.Instance.IsWaveCompleted += OnWaveCompleted;
        LevelManager.Instance.IsLevelCompleted += OnLevelCompleted;
    }

    private void OnWaveCompleted()
    {
        _enemyUnitSpawner.Init(LevelManager.Instance.CurrentWave);
    }
    
    private void OnLevelCompleted()
    {
        IsBattleGoing = false;
    }

    public void StartBattle()
    {
        _playerUnitSpawner.Init();
        _enemyUnitSpawner.Init(LevelManager.Instance.CurrentWave);
        _castle.Init();

        IsBattleGoing = true;
        
        UIManager.Instance.GameplayScreen.Show();
        
        Wallet.Instance.ResetExp();
    }

    public void LoseBattle()
    {
        IsBattleGoing = false;

        Time.timeScale = 1f;
        
        UIManager.Instance.LoseScreen.Show();
    }
}
