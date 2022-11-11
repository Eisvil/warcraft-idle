using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainScreenMenuButton : MenuButton
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Vector2 _selectedIconPosition;
    [SerializeField] private Vector2 _deselectedIconPosition;
    
    public override void Select(bool isAnimationNeeded = true)
    {
        if(IsSelected) return;
        
        IsSelected = true;
        
        if(isAnimationNeeded)
        {
            Tween = RectTransform.DOSizeDelta(_selectedSize, _animationDuration);
            Tween = _icon.rectTransform.DOAnchorPos(_selectedIconPosition, _animationDuration);
            Tween = _name.rectTransform.DOScale(Vector3.one, _animationDuration);
        }
        else
        {
            RectTransform.sizeDelta = _selectedSize;
            _icon.rectTransform.anchoredPosition = _selectedIconPosition;
            _name.rectTransform.localScale = Vector3.one;
        }
        
        base.Select(isAnimationNeeded);
    }
    
    public override void Deselect(bool isAnimationNeeded = true)
    {
        if(!IsSelected) return;
        
        IsSelected = false;

        if(isAnimationNeeded)
        {
            Tween = RectTransform.DOSizeDelta(_deselectedSize, _animationDuration);
            Tween = _icon.rectTransform.DOAnchorPos(_deselectedIconPosition, _animationDuration);
            Tween = _name.rectTransform.DOScale(Vector3.zero, _animationDuration);
        }
        else
        {
            RectTransform.sizeDelta = _deselectedSize;
            _icon.rectTransform.anchoredPosition = _deselectedIconPosition;
            _name.rectTransform.localScale = Vector3.zero;
        }
    }
}