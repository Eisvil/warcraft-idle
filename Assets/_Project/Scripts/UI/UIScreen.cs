using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIScreen : MonoBehaviour
{
    protected CanvasGroup CanvasGroup;

    protected virtual void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }
    
    public virtual void Show()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
    }
    
    public virtual void Hide()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
    }
}