using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScaleButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    private float _currentSpeed = 1f;

    public void ChangeTimeScale()
    {
        _currentSpeed = _currentSpeed switch
        {
            1f => 1.5f,
            1.5f => 2f,
            2f => 1f,
            _ => _currentSpeed
        };

        _text.text = "x" + _currentSpeed;

        Time.timeScale = _currentSpeed;
    }
}
