using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveAnnouncement : MonoBehaviour
{
    private TMP_Text _text;
    private Animator _animator;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _animator = GetComponent<Animator>();
    }

    public void Show()
    {
        _text.text = "Wave " + (LevelManager.Instance.CurrentWaveIndex + 1);
        
        _animator.Play("Show");
    }
}
