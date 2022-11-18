using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentPerkUpgradeButton : PerkUpgradeButton
{
    protected override void ShowPriceAndLevel()
    {
        CurrentPrice = PerkManager.Instance.GetPerkPriceForGold(PerkData.Id);

        PriceText.text = CurrentPrice.ToString();

        var perkMultiplier = PerkManager.Instance.PerkMultiplier;

        switch (PerkData.Id)
        {
            case PerkName.CastleHealth:
                perkMultiplier *= 20;
                
                MultiplierText.text = (10 * (1 + PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.HealthRegen:
                perkMultiplier *= 8;
                
                MultiplierText.text = (0.5f * (1 + PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            default:
                MultiplierText.text = (PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier * 100) + "%";
                break;
        }
    }

    public override void TryUpgradePerk()
    {
        if (!Wallet.Instance.TrySpendGold(CurrentPrice)) return;
        
        PerkManager.Instance.UpgradePermanentPerk(PerkData.Id);
            
        ShowPriceAndLevel();
    }
}
