using UnityEngine;


public class GemDisplayer : Displayer
{
    protected override void Init()
    {
        Display(Wallet.Instance.Gem);
    }
    
    private void Start()
    {
        Wallet.Instance.IsGemChanged += Display;
    }

    private void OnDestroy()
    {
        Wallet.Instance.IsGoldChanged -= Display;
    }
}
