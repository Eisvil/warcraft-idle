using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerkManager : Singleton<PerkManager>
{
    [SerializeField] private PerkData[] _attackPerks;
    [SerializeField] private PerkData[] _defensePerks;
    [SerializeField] private PerkData[] _incomePerks;
    [Space(10f)] 
    [SerializeField] private int[] _pricesForGold;
    [SerializeField] private int[] _pricesForExp;
    [Space(10f)]
    [SerializeField] private PerkPanel[] _attackPerkPanels;
    [SerializeField] private PerkPanel[] _defensePerkPanels;
    [SerializeField] private PerkPanel[] _incomePerkPanels;
    
    private int[] _temporaryPerkLevels;

    public float PerkMultiplier { get; private set; } = 0.05f;
    public event UnityAction<PerkName> IsAnyPerkUpgraded; 

    private void Init()
    {
        _temporaryPerkLevels = new int[Enum.GetNames(typeof(PerkName)).Length];

        foreach (var perkPanel in _attackPerkPanels)
        {
            perkPanel.Init(_attackPerks);
        }
        
        foreach (var perkPanel in _defensePerkPanels)
        {
            perkPanel.Init(_defensePerks);
        }
        
        foreach (var perkPanel in _incomePerkPanels)
        {
            perkPanel.Init(_incomePerks);
        }
    }

    private void Start()
    {
        Init();
    }

    public void UpgradeTemporaryPerk(PerkName perkName)
    {
        _temporaryPerkLevels[(int)perkName]++;
        
        IsAnyPerkUpgraded?.Invoke(perkName);
    }
    
    public void UpgradePermanentPerk(PerkName perkName)
    {
        DataManager.Instance.Data.PermanentPerkLevels[(int)perkName]++;
        
        DataManager.Instance.Save();
    }
    
    public int GetTemporaryPerkLevel(PerkName perkName)
    {
        return _temporaryPerkLevels[(int)perkName];
    }
    
    public int GetPermanentPerkLevel(PerkName perkName)
    {
        return DataManager.Instance.Data.PermanentPerkLevels[(int)perkName];
    }

    public int GetPerkLevel(PerkName perkName)
    {
        return DataManager.Instance.Data.PermanentPerkLevels[(int)perkName] + _temporaryPerkLevels[(int)perkName];
    }

    public int GetPerkPriceForGold(PerkName perkName)
    {
        return _pricesForGold[GetPermanentPerkLevel(perkName)];
    }
    
    public int GetPerkPriceForExp(PerkName perkName)
    {
        return _pricesForExp[GetTemporaryPerkLevel(perkName)];
    }

    public void UpdateStats()
    {
        foreach (var perkPanel in _attackPerkPanels)
        {
            perkPanel.ShowLevelsAndPrices(_attackPerks);
        }
        
        foreach (var perkPanel in _defensePerkPanels)
        {
            perkPanel.ShowLevelsAndPrices(_defensePerks);
        }
        
        foreach (var perkPanel in _incomePerkPanels)
        {
            perkPanel.ShowLevelsAndPrices(_incomePerks);
        }
    }

    public void Reset()
    {
        for (var i = 0; i < _temporaryPerkLevels.Length; i++)
        {
            _temporaryPerkLevels[i] = 0;
        }
    }
}

[Serializable]
public enum PerkName
{
    UnitHealth,
    Damage,
    AttackSpeed,
    MoveSpeed,
    CritChance,
    CritMultiplier,
    CastleHealth,
    HealthRegen,
    ExpPerEnemy,
    GoldMining,
    ExpPerWave,
    GoldPerWave
}