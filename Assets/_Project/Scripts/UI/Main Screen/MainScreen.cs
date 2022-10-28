using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : UIScreen
{
    public void Play()
    {
        BattleManager.Instance.StartBattle();
        Disable();
    }
}
