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
            Tween = RectTransform.DOSizeDelta(_selectedSize, AnimationDuration);
            Tween = _icon.rectTransform.DOAnchorPos(_selectedIconPosition, AnimationDuration);
            Tween = _name.rectTransform.DOScale(Vector3.one, AnimationDuration);
        }
        else
        {
            RectTransform.sizeDelta = _selectedSize;
            _icon.rectTransform.anchoredPosition = _selectedIconPosition;
            _name.rectTransform.localScale = Vector3.one;
        }
        
        base.Select(isAnimationNeeded);
    }
    
    public override void Deselect(bool isAnimationNeeded = true, bool isDeselectYourself = false)
    {
        if(!IsSelected) return;
        
        IsSelected = false;

        if(isAnimationNeeded)
        {
            Tween = RectTransform.DOSizeDelta(_deselectedSize, AnimationDuration);
            Tween = _icon.rectTransform.DOAnchorPos(_deselectedIconPosition, AnimationDuration);
            Tween = _name.rectTransform.DOScale(Vector3.zero, AnimationDuration);
        }
        else
        {
            RectTransform.sizeDelta = _deselectedSize;
            _icon.rectTransform.anchoredPosition = _deselectedIconPosition;
            _name.rectTransform.localScale = Vector3.zero;
        }
    }
}