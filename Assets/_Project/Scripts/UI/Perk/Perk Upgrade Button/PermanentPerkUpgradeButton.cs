using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentPerkUpgradeButton : PerkUpgradeButton
{
    protected override void ShowPriceAndLevel()
    {
        CurrentPrice = PerkManager.Instance.GetPerkPriceForGold(PerkData.Id);

        PriceText.text = CurrentPrice.ToString();
        LevelText.text = "Level: " + (PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) + 1);
    }

    public override void TryUpgradePerk()
    {
        if (!Wallet.Instance.TrySpendGold(CurrentPrice)) return;
        
        PerkManager.Instance.UpgradePermanentPerk(PerkData.Id);
            
        ShowPriceAndLevel();
    }
}
