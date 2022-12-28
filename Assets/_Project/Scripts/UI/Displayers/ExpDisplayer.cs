using UnityEngine;


public class ExpDisplayer : Displayer
{
    protected override void Init()
    {
        Display(Wallet.Instance.Experience);
    }

    private void OnEnable()
    {
        Wallet.Instance.IsExpChanged += Display;
    }

    private void OnDisable()
    {
        Wallet.Instance.IsExpChanged -= Display;
    }
}
