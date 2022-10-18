using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckManager : Singleton<PlayerDeckManager>
{
    [SerializeField] private int[] _selectedUnitsId;

    public int[] SelectedUnitsId => _selectedUnitsId;
}
