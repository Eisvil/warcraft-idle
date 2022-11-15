using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelData[] _levels;

    private LevelData _currentLevel;
    private WaveData _currentWave;
    private int _currentWaveIndex;

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

        _currentWaveIndex = 0;
        _currentWave = _currentLevel.Waves[_currentWaveIndex];
    }

    public void WaveComplete()
    {
        if (_currentWaveIndex + 1 >= _currentLevel.Waves.Length)
        {
            CompleteLevel();
        }
        else
        {
            _currentWaveIndex++;
            _currentWave = _currentLevel.Waves[_currentWaveIndex];
            
            IsWaveCompleted?.Invoke();
        }
    }
}
