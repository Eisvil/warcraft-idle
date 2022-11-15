using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkPanel : MonoBehaviour
{
    [SerializeField] private PerkUpgradeButton[] _upgradeButtons;
    
    public void Init(PerkData[] perksData)
    {
        for (var i = 0; i < perksData.Length; i++)
        {
            _upgradeButtons[i].gameObject.SetActive(true);
            
            _upgradeButtons[i].Init(perksData[i]);
        }
    }
}
