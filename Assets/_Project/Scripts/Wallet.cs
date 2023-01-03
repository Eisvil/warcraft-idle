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

    public event UnityAction<float> IsGoldChanged;
    public event UnityAction<float> IsGemChanged;
    public event UnityAction<float> IsExpChanged;

    public float Gold => _gold;
    public int Gem => _gem;
    public float Experience => _experience;

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Load()
    {
        _gold = DataManager.Instance.Data.Gold;
        _gem = DataManager.Instance.Data.Gem;

        yield return new WaitForSeconds(0.3f);
    }

    public bool TrySpendGold(int price)
    {
        if (_gold >= price)
        {
            _gold -= price;

            DataManager.Instance.Data.Gold = _gold;
            DataManager.Instance.Save();

            IsGoldChanged?.Invoke(_gold);

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

            IsExpChanged?.Invoke(_experience);
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddGold(float value, bool isFromMining = true)
    {
        _gold += value * (1 + (isFromMining ? PerkManager.Instance.GetPerkLevel(PerkName.GoldMining) / 2.5f : PerkManager.Instance.GetPerkLevel(PerkName.GoldPerWave) * 20) * PerkManager.Instance.PerkMultiplier);
        
        DataManager.Instance.Data.Gold = _gold;
        DataManager.Instance.Save();
        
        IsGoldChanged?.Invoke(_gold);
    }
    
    public void AddExp(float value, bool isPerEnemy = true)
    {
        _experience += value * (1 + (isPerEnemy ? PerkManager.Instance.GetPerkLevel(PerkName.ExpPerEnemy) * 2 : PerkManager.Instance.GetPerkLevel(PerkName.ExpPerWave) * 20) * PerkManager.Instance.PerkMultiplier);
        
        IsExpChanged?.Invoke(_experience);
    }

    public void ResetExp()
    {
        _experience = 0;
        
        IsExpChanged?.Invoke(_experience);
    }
}
