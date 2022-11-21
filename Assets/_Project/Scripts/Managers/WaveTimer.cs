using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] private float _waveLenght;
    [SerializeField] private WaveProgressBar _waveProgressBar;

    private Coroutine _coroutine;
    private float _timer;

    private IEnumerator StartTimerCoroutine()
    {
        while (_timer < _waveLenght)
        {
            _timer += Time.deltaTime;
            
            _waveProgressBar.Show(_timer, _waveLenght);

            yield return null;
        }

        LevelManager.Instance.WaveComplete();

        StartTimer();
    }

    public void StartTimer()
    {
        _waveProgressBar.SetLevel();
        
        UIManager.Instance.GameplayScreen.WaveAnnouncement.Show();
        
        _timer = 0;
        
        _coroutine = StartCoroutine(StartTimerCoroutine());
    }

    public void StopTimer()
    {
        StopCoroutine(_coroutine);
    }
}
