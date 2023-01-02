using System;
using UnityEngine;

public class GoldDisplayer : Displayer
{
    protected override void Init()
    {
        Display(Wallet.Instance.Gold);
    }

    private void OnEnable()
    {
        Wallet.Instance.IsGoldChanged += Display;
    }

    private void OnDisable()
    {
        Wallet.Instance.IsGoldChanged -= Display;
    }
}
