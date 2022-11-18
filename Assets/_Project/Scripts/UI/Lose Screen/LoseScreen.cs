using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : UIScreen
{
    public void Continue()
    {
        SceneManager.LoadScene(0);
    }
}
