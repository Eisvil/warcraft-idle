using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PerkUpgradeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;
    [SerializeField] protected TMP_Text MultiplierText;
    [SerializeField] protected TMP_Text PriceText;

    private Button _button;
    
    protected PerkData PerkData;
    protected int CurrentPrice;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Init(PerkData perkData)
    {
        PerkData = perkData;
        
        _name.text = perkData.Name;
        _icon.sprite = perkData.Icon;

        ShowPriceAndLevel();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(TryUpgradePerk);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryUpgradePerk);
    }

    protected abstract void ShowPriceAndLevel();

    public abstract void TryUpgradePerk();
}
