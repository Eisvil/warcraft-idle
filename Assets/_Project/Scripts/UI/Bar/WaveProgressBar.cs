using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveProgressBar : Bar
{
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _currentWave;
    [SerializeField] private TMP_Text _nextWave;

    public void SetLevel()
    {
        _level.text = "Level " + (LevelManager.Instance.CurrentLevelIndex + 1);
    }
    
    public void Show(float currentTime, float waveLenght)
    {
        Show(currentTime, waveLenght, false);

        _currentWave.text = (LevelManager.Instance.CurrentWaveIndex + 1).ToString();
        _nextWave.text = (LevelManager.Instance.CurrentWaveIndex + 2).ToString();
    }
}
