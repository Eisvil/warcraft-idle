using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PerkMenu : Menu
{
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private GameObject _perkPanelsContainer;
    [SerializeField] private bool _needSelectFromStart;

    private bool _isSelected;

    protected override void OnEnable()
    {
        base.OnEnable();
        
        foreach (var menuButton in MenuButtons)
        {
            menuButton.IsDeselecting += OnDeselecting;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        foreach (var menuButton in MenuButtons)
        {
            menuButton.IsDeselecting -= OnDeselecting;
        }
    }

    private void Start()
    {
        if (_needSelectFromStart)
        {
            MenuButtons[0].Select();
        }
    }

    private void OnDeselecting(MenuButton button, bool isAnimationNeeded)
    {
        foreach (var menuButton in MenuButtons)
        {
            menuButton.Deselect(isAnimationNeeded);
        }
        
        _perkPanelsContainer.SetActive(false);
        
        _isSelected = false;
        
        _cameraMover.MoveTo(CameraPosition.Default);
    }

    protected override void OnSelecting(MenuButton button, bool isAnimationNeeded)
    {
        foreach (var menuButton in MenuButtons.Where(menuButton => menuButton != button))
        {
            menuButton.Deselect(isAnimationNeeded);
        }
        
        if(!_isSelected)
        {
            _perkPanelsContainer.SetActive(true);

            _isSelected = true;
            
            _cameraMover.MoveTo(CameraPosition.WithPerks);
            
            ShowPerkPanel(MenuButtons.IndexOf(button), false);
        }
        else
        {
            ShowPerkPanel(MenuButtons.IndexOf(button));
        }
    }
}
