using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckManager : Singleton<PlayerDeckManager>
{
    [SerializeField] private int[] _selectedUnitsId;
    [SerializeField] private int[] _selectedUnitLevels;

    public int[] SelectedUnitsId => _selectedUnitsId;

    public int[] SelectedUnitLevels => _selectedUnitLevels;
}
