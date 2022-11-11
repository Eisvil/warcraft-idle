using DG.Tweening;
using UnityEngine;

public class GameplayScreenMenuButton : MenuButton
{
    [SerializeField] private Vector3 _selectedIconScale;
    [SerializeField] private Vector3 _deselectedIconScale;
    
    public override void Select(bool isAnimationNeeded = true)
    {
        IsSelected = true;
        
        if(isAnimationNeeded)
        {
            Tween = RectTransform.DOSizeDelta(_selectedSize, _animationDuration);
            Tween = _icon.rectTransform.DOScale(_selectedIconScale, _animationDuration);
        }
        else
        {
            RectTransform.sizeDelta = _selectedSize;
            _icon.rectTransform.localScale = _selectedIconScale;
        }
        
        base.Select(isAnimationNeeded);
    }
    
    public override void Deselect(bool isAnimationNeeded = true)
    {
        IsSelected = false;

        if(isAnimationNeeded)
        {
            Tween = RectTransform.DOSizeDelta(_deselectedSize, _animationDuration);
            Tween = _icon.rectTransform.DOScale(_deselectedIconScale, _animationDuration);
        }
        else
        {
            RectTransform.sizeDelta = _deselectedSize;
            _icon.rectTransform.localScale = _deselectedIconScale;
        }
    }

    public void OnClick()
    {
        if (IsSelected)
            Deselect();
        else
            Select();
    }
}