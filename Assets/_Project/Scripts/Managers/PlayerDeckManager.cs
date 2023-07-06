using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDeckManager : Singleton<PlayerDeckManager>
{
    
    
    public int[] SelectedUnitsId => DataManager.Instance.Data.SelectedUnitIds.ToArray();
    
    
    public int[] GetSelectedUnitLevels()
    {
        return SelectedUnitsId.Select(selectedUnitId => DataManager.Instance.Data.UnlockedUnitLevels[DataManager.Instance.Data.UnlockedUnitIds.IndexOf(selectedUnitId)]).ToArray();
    }
    
}
