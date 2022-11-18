using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMiner : MonoBehaviour
{
    [SerializeField] private float _mineTime;
    [SerializeField] private int _goldPerMine = 1;
    
    private float _timer;

    private void Update()
    {
        MineGold();
    }

    private void MineGold()
    {
        if(!BattleManager.Instance.IsBattleGoing) return;
        
        _timer += Time.deltaTime;
        
        if(_timer < _mineTime) return;

        _timer = 0f;
        Wallet.Instance.AddGold(_goldPerMine);
    }
}
