using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentPerkUpgradeButton : PerkUpgradeButton
{
    protected override void OnEnable()
    {
        base.OnEnable();
        
        Wallet.Instance.IsGoldChanged += CheckBalance;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        Wallet.Instance.IsGoldChanged -= CheckBalance;
    }

    public override void ShowPriceAndStats()
    {
        CurrentPrice = PerkManager.Instance.GetPerkPriceForGold(PerkData.Id);
        
        CheckBalance(Wallet.Instance.Gold);

        PriceText.text = CurrentPrice.ToString();

        var perkMultiplier = PerkManager.Instance.PerkMultiplier;

        switch (PerkData.Id)
        {
            case PerkName.CastleHealth:
                perkMultiplier *= 10f;
                
                MultiplierText.text = (10 * (1 + PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.HealthRegen:
                perkMultiplier *= 4f;
                
                MultiplierText.text = (0.5f * (1 + PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.ExpPerEnemy:
                perkMultiplier *= 2f;
                
                MultiplierText.text = (PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier * 100) + "%";
                break;
            case PerkName.GoldMining:
                perkMultiplier /= 2.5f;
                
                MultiplierText.text = (1 * (1 + PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.GoldPerWave:
                perkMultiplier *= 20f;
                
                MultiplierText.text = (LevelManager.Instance.GoldRewardForWave * (1 + PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.ExpPerWave:
                perkMultiplier *= 20f;
                
                MultiplierText.text = (LevelManager.Instance.ExpRewardForWave * (1 + PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.AttackSpeed:
                perkMultiplier /= 2.5f;
                
                MultiplierText.text = (PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier * 100) + "%";
                break;
            case PerkName.CritChance:
                perkMultiplier *= 10f;
                
                MultiplierText.text = (PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier * 100) + "%";
                break;
            case PerkName.CritMultiplier:
                perkMultiplier *= 2f;
                
                MultiplierText.text = (PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier * 100) + "%";
                break;
            default:
                MultiplierText.text = (PerkManager.Instance.GetPermanentPerkLevel(PerkData.Id) * perkMultiplier * 100) + "%";
                break;
        }
    }

    public override void TryUpgradePerk()
    {
        if (!Wallet.Instance.TrySpendGold(CurrentPrice)) return;
        
        SoundManager.Instance.PlaySound(SoundName.ButtonClick);

        PerkManager.Instance.UpgradePermanentPerk(PerkData.Id);
            
        ShowPriceAndStats();
    }
}
