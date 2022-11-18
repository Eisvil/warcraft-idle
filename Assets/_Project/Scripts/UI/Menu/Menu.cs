using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected List<RectTransform> PanelsRectTransform;
    [SerializeField] protected Transform[] MovePoints;
    [SerializeField] private float _scrollSpeed;
    
    private Tween _tween;
    
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

    protected void ShowPerkPanel(int index, bool isAnimationNeeded = true)
    {
        if (isAnimationNeeded)
        {
            for (var i = 0; i < PanelsRectTransform.Count; i++)
            {
                _tween = PanelsRectTransform[i].DOMove(MovePoints[i + MovePoints.Length / 2 - index].position, _scrollSpeed);
            }
        }
        else
        {
            for (var i = 0; i < PanelsRectTransform.Count; i++)
            {
                PanelsRectTransform[i].position = MovePoints[i + MovePoints.Length / 2 - index].position;
            }
        }
    }
    
    protected abstract void OnSelecting(MenuButton button, bool isAnimationNeeded);
}
