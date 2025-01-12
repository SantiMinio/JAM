using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel_ScaleTransition : UIPanelTransition
{
    [Header("Transition Things")]
    [SerializeField] float openTime = 1;
    [SerializeField] float closeTime = 1;

    [SerializeField] CanvasGroup myCanvas;

    public Vector3 closePosition;
    Vector3 openPosition;

    [SerializeField] bool scaleX = true;
    [SerializeField] bool scaleY = true;
    [SerializeField] bool scaleZ = false;

    float timer;

    protected override void OnCloseAbs()
    {
        timer = 0;
    }

    protected override void OnCloseOverAbs()
    {
        transform.localScale = closePosition;
        if (myCanvas != null)
        {
            myCanvas.alpha = 0;
            myCanvas.blocksRaycasts = false;
            myCanvas.interactable = false;
        }
    }

    protected override void OnClosingAbs(float deltaTime)
    {
        timer += deltaTime;

        transform.localScale = Vector3.Lerp(openPosition, closePosition, timer / closeTime);

        if (timer >= closeTime)
            CloseOver();
    }

    protected override void OnOpenAbs()
    {
        timer = 0;
        if(myCanvas!= null)
        {
            myCanvas.alpha = 1;
            myCanvas.blocksRaycasts = true;
            myCanvas.interactable = true;
        }
    }

    protected override void OnOpeningAbs(float deltaTime)
    {
        timer += deltaTime;
        transform.localScale = Vector3.Lerp(closePosition, openPosition, timer / openTime);

        if (timer >= openTime)
            OpenOver();
    }

    protected override void OnOpenOverAbs()
    {
        transform.localScale = openPosition;
        if (myCanvas != null)
        {
            myCanvas.alpha = 1;
            myCanvas.blocksRaycasts = true;
            myCanvas.interactable = true;
        }
    }

    protected override void Initialize()
    {
        openPosition = transform.localScale;
        closePosition = new Vector3(scaleX ? closePosition.x : transform.localScale.x, scaleY ? closePosition.y : transform.localScale.y, scaleZ ? closePosition.z : transform.localScale.z);
    }
}
