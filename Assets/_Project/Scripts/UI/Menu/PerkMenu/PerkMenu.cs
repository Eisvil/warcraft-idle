using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PerkMenu : Menu
{
    [SerializeField] private GameObject _perkPanelsContainer;
    [SerializeField] private PerkPanel[] _perkPanels;
    [SerializeField] private List<RectTransform> _perkPanelsRectTransform;
    [SerializeField] private Transform[] _movePoints;

    private Tween _tween;
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

    private void OnDeselecting(MenuButton button, bool isAnimationNeeded)
    {
        foreach (var menuButton in MenuButtons)
        {
            menuButton.Deselect(isAnimationNeeded);
        }
        
        _perkPanelsContainer.SetActive(false);
        
        _isSelected = false;
    }

    private void ShowPerkPanel(int index, bool isAnimationNeeded = true)
    {
        if (isAnimationNeeded)
        {
            for (var i = 0; i < _perkPanelsRectTransform.Count; i++)
            {
                _tween = _perkPanelsRectTransform[i].DOMove(_movePoints[i + _movePoints.Length / 2 - index].position, 0.5f);
            }
        }
        else
        {
            for (var i = 0; i < _perkPanelsRectTransform.Count; i++)
            {
                _perkPanelsRectTransform[i].position = _movePoints[i + _movePoints.Length / 2 - index].position;
            }
        }
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
            
            ShowPerkPanel(MenuButtons.IndexOf(button), false);
        }
        else
        {
            ShowPerkPanel(MenuButtons.IndexOf(button));
        }
    }
}
