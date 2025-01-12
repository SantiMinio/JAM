using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel_AlphaTransition : UIPanelTransition
{
    [SerializeField] CanvasGroup group = null;

    [Header("Transition Properties")]
    [SerializeField] float openTime = 2f;
    [SerializeField] float closeTime = 2f;
    float timer;

    protected override void Initialize()
    {
    }

    protected override void OnCloseAbs()
    {
        timer = 0;
        group.blocksRaycasts = false;
        group.interactable = false;
    }

    protected override void OnCloseOverAbs()
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
        group.interactable = false;
    }

    protected override void OnClosingAbs(float deltaTime)
    {
        timer += deltaTime;

        group.alpha =  1 - timer / closeTime;

        if (timer >= closeTime)
            CloseOver();
    }

    protected override void OnOpenAbs()
    {
        timer = 0;
        group.blocksRaycasts = true;
        group.interactable = true;
    }

    protected override void OnOpeningAbs(float deltaTime)
    {
        timer += deltaTime;

        group.alpha = timer / openTime;

        if (timer >= openTime)
            OpenOver();
    }

    protected override void OnOpenOverAbs()
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;
    }
}
