using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Button))]
public abstract class MenuButton : MonoBehaviour
{
    [SerializeField] protected Image _icon;
    [Header("References")]
    [SerializeField] protected float _animationDuration;
    [SerializeField] protected Vector2 _selectedSize;
    [SerializeField] protected Vector2 _deselectedSize;

    protected RectTransform RectTransform;
    protected Button Button;
    protected Tween Tween;
    protected bool IsSelected;
    protected float AnimationDuration => _animationDuration * Time.timeScale;

    public event UnityAction<MenuButton, bool> IsSelecting;
    public event UnityAction<MenuButton, bool> IsDeselecting;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        Button = GetComponent<Button>();
    }

    public virtual void Select(bool isAnimationNeeded = true)
    {
        IsSelecting?.Invoke(this, isAnimationNeeded);
    }
    
    public virtual void Deselect(bool isAnimationNeeded = true, bool isDeselectYourself = false)
    {
        IsDeselecting?.Invoke(this, isAnimationNeeded);
    }
}
