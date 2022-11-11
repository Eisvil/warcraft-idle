using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    protected List<MenuButton> MenuButtons;

    private void Awake()
    {
        MenuButtons = GetComponentsInChildren<MenuButton>().ToList();
    }

    protected virtual void OnEnable()
    {
        foreach (var menuButton in MenuButtons)
        {
            menuButton.IsSelecting += OnSelecting;
        }
    }

    protected virtual void OnDisable()
    {
        foreach (var menuButton in MenuButtons)
        {
            menuButton.IsSelecting -= OnSelecting;
        }
    }

    protected virtual void OnSelecting(MenuButton button, bool isAnimationNeeded)
    {
        foreach (var menuButton in MenuButtons.Where(menuButton => menuButton != button))
        {
            menuButton.Deselect(isAnimationNeeded);
        }
    }
}
