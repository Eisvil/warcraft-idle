using UnityEngine;


public class ExpDisplayer : Displayer
{
    protected override void Init()
    {
        Display(Wallet.Instance.Experience);
    }

    protected override void Start()
    {
        base.Start();
        
        Wallet.Instance.IsExpChanged += Display;
    }

    private void OnDestroy()
    {
        Wallet.Instance.IsExpChanged -= Display;
    }
}
