using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckSlot : MonoBehaviour
{
    [SerializeField] private GameObject[] _slotStateObjects;
    [Header("SlotsToFill")] 
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    
    public DeckSlotState CurrentState { get; private set; }

    public void Init(UnitData unitData)
    {
        _icon.sprite = unitData.Icon;
        _name.text = unitData.Name;
    }
}

public enum DeckSlotState
{
    Locked,
    Empty,
    WithUnit
}