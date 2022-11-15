using System;
using System.Collections;
using TMPro;
using UnityEngine;


public abstract class Displayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    private float _currentValue;

    protected abstract void Init();

    private void Start()
    {
        Init();
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
        /*if (isAnimationNeeded)
        {
            StartCoroutine(DisplayCoroutine(value));
        }
        else
        {
            _currentValue = value;
            _text.text = value.ToString();
        }*/
        
        _currentValue = value;
        _text.text = value.ToString();
    }
}
