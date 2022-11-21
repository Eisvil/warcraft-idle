using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelData[] _levels;

    private LevelData _currentLevel;
    private WaveData _currentWave;
    
    public int CurrentLevelIndex { get; private set; }
    public int CurrentWaveIndex { get; private set; }
    public float ExpRewardForWave { get; private set; } = 3;
    public float GoldRewardForWave { get; private set; } = 3;
    public WaveData CurrentWave => _currentWave;
    public event UnityAction IsWaveCompleted; 
    public event UnityAction IsLevelCompleted; 


    private void CompleteLevel()
    {
        IsLevelCompleted?.Invoke();
    }
    
    public void SelectLevel(int index)
    {
        _currentLevel = _levels[index];
        CurrentLevelIndex = index;

        CurrentWaveIndex = 0;
        _currentWave = _currentLevel.Waves[CurrentWaveIndex];
    }

    public void WaveComplete()
    {
        if (CurrentWaveIndex + 1 >= _currentLevel.Waves.Length)
        {
            CompleteLevel();
        }
        else
        {
            CurrentWaveIndex++;
            _currentWave = _currentLevel.Waves[CurrentWaveIndex];
            
            Wallet.Instance.AddExp(ExpRewardForWave, false);
            Wallet.Instance.AddGold(GoldRewardForWave, false);
            
            IsWaveCompleted?.Invoke();
        }
    }
}
