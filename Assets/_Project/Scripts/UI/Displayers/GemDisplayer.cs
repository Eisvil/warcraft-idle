using UnityEngine;


public class GemDisplayer : Displayer
{
    protected override void Init()
    {
        Display(Wallet.Instance.Gem);
    }

    private void OnEnable()
    {
        Wallet.Instance.IsGemChanged += Display;
    }

    private void OnDisable()
    {
        Wallet.Instance.IsGemChanged -= Display;
    }
}
