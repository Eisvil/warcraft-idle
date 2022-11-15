using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : UIScreen
{
    public void Play()
    {
        LevelManager.Instance.SelectLevel(0);
        
        BattleManager.Instance.StartBattle();
        
        Hide();
    }
}
