using System;
using UnityEngine;

public class GoldDisplayer : Displayer
{
    protected override void Init()
    {
        Display(Wallet.Instance.Gold);
    }
    
    private void Start()
    {
        Wallet.Instance.IsGoldChanged += Display;
    }

    private void OnDestroy()
    {
        Wallet.Instance.IsGoldChanged -= Display;
    }

    
}
