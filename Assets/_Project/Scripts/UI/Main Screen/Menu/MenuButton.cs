using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;

    [Header("References")]
    [SerializeField] private float _animationDuration;
    [SerializeField] private Vector2 _selectedSize;
    [SerializeField] private Vector2 _deselectedSize;
    [SerializeField] private Vector2 _selectedIconPosition;
    [SerializeField] private Vector2 _deselectedIconPosition;

    private RectTransform _rectTransform;
    private Button _button;
    private Tween _tween;
    private bool _isSelected;

    public event UnityAction<MenuButton, bool> IsSelected; 

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _button = GetComponent<Button>();
    }

    public void Select(bool isAnimationNeeded = true)
    {
        if(_isSelected) return;
        
        _isSelected = true;
        
        if(isAnimationNeeded)
        {
            _tween = _rectTransform.DOSizeDelta(_selectedSize, _animationDuration);
            _tween = _icon.rectTransform.DOAnchorPos(_selectedIconPosition, _animationDuration);
            _tween = _name.rectTransform.DOScale(Vector3.one, _animationDuration);
        }
        else
        {
            _rectTransform.sizeDelta = _selectedSize;
            _icon.rectTransform.anchoredPosition = _selectedIconPosition;
            _name.rectTransform.localScale = Vector3.one;
        }

        IsSelected?.Invoke(this, isAnimationNeeded);
    }
    
    public void Deselect(bool isAnimationNeeded = true)
    {
        if(!_isSelected) return;
        
        _isSelected = false;

        if(isAnimationNeeded)
        {
            _tween = _rectTransform.DOSizeDelta(_deselectedSize, _animationDuration);
            _tween = _icon.rectTransform.DOAnchorPos(_deselectedIconPosition, _animationDuration);
            _tween = _name.rectTransform.DOScale(Vector3.zero, _animationDuration);
        }
        else
        {
            _rectTransform.sizeDelta = _deselectedSize;
            _icon.rectTransform.anchoredPosition = _deselectedIconPosition;
            _name.rectTransform.localScale = Vector3.zero;
        }
    }
}
