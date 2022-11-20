using UnityEngine;


public class GemDisplayer : Displayer
{
    protected override void Init()
    {
        Display(Wallet.Instance.Gem);
    }

    protected override void Start()
    {
        base.Start();
        
        Wallet.Instance.IsGemChanged += Display;
    }

    private void OnDestroy()
    {
        Wallet.Instance.IsGoldChanged -= Display;
    }
}
