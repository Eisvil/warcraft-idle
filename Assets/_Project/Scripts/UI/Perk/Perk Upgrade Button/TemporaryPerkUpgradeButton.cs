using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPerkUpgradeButton : PerkUpgradeButton
{
    protected override void ShowPriceAndLevel()
    {
        CurrentPrice = PerkManager.Instance.GetPerkPriceForExp(PerkData.Id);

        PriceText.text = CurrentPrice.ToString();
        LevelText.text = "Level: " + (PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) + PerkManager.Instance.GetTemporaryPerkLevel(PerkData.Id) + 1);
    }

    public override void TryUpgradePerk()
    {
        if (!Wallet.Instance.TrySpendExp(CurrentPrice)) return;
        
        PerkManager.Instance.UpgradeTemporaryPerk(PerkData.Id);
            
        ShowPriceAndLevel();
    }
}
