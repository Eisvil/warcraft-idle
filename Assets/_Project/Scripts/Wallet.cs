using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : Singleton<Wallet>
{
    private float _gold;
    private int _gem;
    private float _experience;

    public event UnityAction<float, bool> IsGoldChanged;
    public event UnityAction<float, bool> IsGemChanged;
    public event UnityAction<float, bool> IsExpChanged;

    public float Gold => _gold;
    public int Gem => _gem;
    public float Experience => _experience;

    public void Init()
    {
        _gold = DataManager.Instance.Data.Gold;
        _gem = DataManager.Instance.Data.Gem;
        
        IsGoldChanged?.Invoke(_gold, false);
        IsGemChanged?.Invoke(_gem, false);
    }
    
    private void Start()
    {
        Init();
    }

    public bool TrySpendGold(int price)
    {
        if (_gold >= price)
        {
            _gold -= price;

            DataManager.Instance.Data.Gold = _gold;
            DataManager.Instance.Save();

            IsGoldChanged?.Invoke(_gold, true);

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TrySpendExp(int price)
    {
        if (_experience >= price)
        {
            _experience -= price;

            IsExpChanged?.Invoke(_experience, true);
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddGold(int value, bool isFromMining = true)
    {
        _gold += value * (1 + (isFromMining ? PerkManager.Instance.GetPerkLevel(PerkName.GoldMining) : PerkManager.Instance.GetPerkLevel(PerkName.GoldPerLevel)) * PerkManager.Instance.PerkMultiplier);
        
        DataManager.Instance.Data.Gold = _gold;
        DataManager.Instance.Save();
        
        IsGoldChanged?.Invoke(_gold, true);
    }
    
    public void AddExp(int value, bool isPerEnemy = true)
    {
        _experience += value * (1 + (isPerEnemy ? PerkManager.Instance.GetPerkLevel(PerkName.ExpPerEnemy) : PerkManager.Instance.GetPerkLevel(PerkName.ExpPerLevel)) * PerkManager.Instance.PerkMultiplier);
        
        IsExpChanged?.Invoke(_experience, true);
    }

    public void ResetExp()
    {
        _experience = 0;
        
        IsExpChanged?.Invoke(_experience, false);
    }
}
