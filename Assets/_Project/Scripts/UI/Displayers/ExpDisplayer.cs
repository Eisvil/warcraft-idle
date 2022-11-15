using UnityEngine;


public class ExpDisplayer : Displayer
{
    protected override void Init()
    {
        Display(Wallet.Instance.Experience);
    }
    
    private void Start()
    {
        Wallet.Instance.IsExpChanged += Display;
    }

    private void OnDestroy()
    {
        Wallet.Instance.IsExpChanged -= Display;
    }
}
