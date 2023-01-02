using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPerkUpgradeButton : PerkUpgradeButton
{
    protected override void OnEnable()
    {
        base.OnEnable();
        
        Wallet.Instance.IsExpChanged += CheckBalance;
        
        ShowPriceAndStats();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        Wallet.Instance.IsExpChanged -= CheckBalance;
    }

    public override void ShowPriceAndStats()
    {
        CurrentPrice = PerkManager.Instance.GetPerkPriceForExp(PerkData.Id);
        
        CheckBalance(Wallet.Instance.Experience);

        PriceText.text = CurrentPrice.ToString();
        
        var perkMultiplier = PerkManager.Instance.PerkMultiplier;

        switch (PerkData.Id)
        {
            case PerkName.CastleHealth:
                perkMultiplier *= 20;
                
                MultiplierText.text = (10 * (1 + PerkManager.Instance.GetPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.HealthRegen:
                perkMultiplier *= 8;
                
                MultiplierText.text = (0.5f * (1 + PerkManager.Instance.GetPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.ExpPerEnemy:
                perkMultiplier *= 2;
                
                MultiplierText.text = (PerkManager.Instance.GetPerkLevel(PerkData.Id) * perkMultiplier * 100) + "%";
                break;
            case PerkName.GoldMining:
                perkMultiplier *= 2;
                
                MultiplierText.text = (1 * (1 + PerkManager.Instance.GetPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.GoldPerWave:
                perkMultiplier *= 10;
                
                MultiplierText.text = (LevelManager.Instance.GoldRewardForWave * (1 + PerkManager.Instance.GetPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            case PerkName.ExpPerWave:
                perkMultiplier *= 10;
                
                MultiplierText.text = (LevelManager.Instance.ExpRewardForWave * (1 + PerkManager.Instance.GetPerkLevel(PerkData.Id) * perkMultiplier)).ToString();
                break;
            default:
                MultiplierText.text = (PerkManager.Instance.GetPerkLevel(PerkData.Id) * perkMultiplier * 100) + "%";
                break;
        }
    }

    public override void TryUpgradePerk()
    {
        if (!Wallet.Instance.TrySpendExp(CurrentPrice)) return;
        
        SoundManager.Instance.PlaySound(SoundName.ButtonClick);

        PerkManager.Instance.UpgradeTemporaryPerk(PerkData.Id);
            
        ShowPriceAndStats();
    }
}
