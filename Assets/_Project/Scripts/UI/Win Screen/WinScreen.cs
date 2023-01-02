using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : UIScreen
{
    public void Continue()
    {
        SoundManager.Instance.PlaySound(SoundName.ButtonClick);
        
        SceneManager.LoadScene(1);
    }
}
