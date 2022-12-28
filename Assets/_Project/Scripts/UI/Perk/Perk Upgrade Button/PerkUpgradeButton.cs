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
    [SerializeField] protected Button Button;
    [SerializeField] protected CanvasGroup CanvasGroup;
    
    protected PerkData PerkData;
    protected int CurrentPrice;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init(PerkData perkData)
    {
        PerkData = perkData;
        
        _name.text = perkData.Name;
        _icon.sprite = perkData.Icon;

        ShowPriceAndStats();
    }

    protected virtual void OnEnable()
    {
        Button.onClick.AddListener(TryUpgradePerk);
    }

    protected virtual void OnDisable()
    {
        Button.onClick.RemoveListener(TryUpgradePerk);
    }

    protected void SetActive(bool value)
    {
        if(value)
        {
            CanvasGroup.alpha = 1f;
            CanvasGroup.blocksRaycasts = true;
        }
        else
        {
            CanvasGroup.alpha = 0.4f;
            CanvasGroup.blocksRaycasts = false;
        }
    }

    protected void CheckBalance(float value)
    {
        SetActive(value >= CurrentPrice);
    }
    
    public abstract void ShowPriceAndStats();

    public abstract void TryUpgradePerk();
}
