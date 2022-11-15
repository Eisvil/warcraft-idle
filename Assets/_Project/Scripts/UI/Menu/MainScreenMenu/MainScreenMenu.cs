using System.Linq;
using UnityEngine;

public class MainScreenMenu : Menu
{
    private void Start()
    {
        MenuButtons[MenuButtons.Count / 2].Select(false);
    }

    protected override void OnSelecting(MenuButton button, bool isAnimationNeeded)
    {
        foreach (var menuButton in MenuButtons.Where(menuButton => menuButton != button))
        {
            menuButton.Deselect(isAnimationNeeded);
        }
    }
}