using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : Singleton<Wallet>
{
    [SerializeField] private Displayer _goldDisplayer;
    [SerializeField] private Displayer _gemDisplayer;
    
    private int _gold;
    private int _gem;

    public void Init()
    {
        _gold = DataManager.Instance.Data.Gold;
        _gem = DataManager.Instance.Data.Gem;
        
        _goldDisplayer.Display(_gold);
        _gemDisplayer.Display(_gem);
    }
    
    private void Start()
    {
        Init();
    }
}
