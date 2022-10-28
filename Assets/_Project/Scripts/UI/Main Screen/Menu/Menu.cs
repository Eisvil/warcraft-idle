using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private List<MenuButton> _menuButtons;

    private void Awake()
    {
        _menuButtons = GetComponentsInChildren<MenuButton>().ToList();
    }

    private void OnEnable()
    {
        foreach (var menuButton in _menuButtons)
        {
            menuButton.IsSelected += OnSelected;
        }
    }

    private void OnDisable()
    {
        foreach (var menuButton in _menuButtons)
        {
            menuButton.IsSelected -= OnSelected;
        }
    }

    private void Start()
    {
        _menuButtons[3].Select(false);
    }

    private void OnSelected(MenuButton button, bool isAnimationNeeded)
    {
        foreach (var menuButton in _menuButtons.Where(menuButton => menuButton != button))
        {
            menuButton.Deselect(isAnimationNeeded);
        }
    }
}
