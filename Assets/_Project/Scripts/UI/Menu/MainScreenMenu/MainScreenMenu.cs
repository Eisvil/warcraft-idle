using UnityEngine;

public class MainScreenMenu : Menu
{
    private void Start()
    {
        MenuButtons[MenuButtons.Count / 2].Select(false);
    }
}