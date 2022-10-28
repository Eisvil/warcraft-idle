using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class Displayer : MonoBehaviour
{
    private TMP_Text _text;
    private float _currentValue;
    
    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    private IEnumerator DisplayCoroutine(float value)
    {
        var previousValue = _currentValue;

        _currentValue = value;

        var step = _currentValue - previousValue * Time.deltaTime;

        while (Math.Abs(previousValue - _currentValue) > 0)
        {
            previousValue += step;

            _text.text = previousValue.ToString("#");

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    
    public void Display(float value, bool isAnimationNeeded = false)
    {
        if (isAnimationNeeded)
        {
            StartCoroutine(DisplayCoroutine(value));
        }
        else
        {
            _currentValue = value;
            _text.text = value.ToString("#");
        }
    }
}
