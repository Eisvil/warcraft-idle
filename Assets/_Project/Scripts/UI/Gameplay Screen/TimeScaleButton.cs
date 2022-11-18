using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScaleButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    private float _currentSpeed;

    public void ChangeTimeScale()
    {
        _currentSpeed = _currentSpeed == 1f ? 2f : 1f;

        _text.text = "x" + _currentSpeed;

        Time.timeScale = _currentSpeed;
    }
}
