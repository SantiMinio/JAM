using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel_MoveTransition : UIPanelTransition
{
    [Header("DirToMove")]
    [SerializeField, Range(-1, 1)] int X;
    [SerializeField, Range(-1, 1)] int Y;

    [Header("Transition Things")]
    [SerializeField] float openTime = 1;
    [SerializeField] float closeTime = 1;

    [SerializeField] CanvasGroup myCanvas;

    Vector3 closePosition;
    Vector3 openPosition;

    float timer;

    protected override void OnCloseAbs()
    {
        timer = 0;
    }

    protected override void OnCloseOverAbs()
    {
        transform.localPosition = closePosition;
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

        transform.localPosition = Vector3.Lerp(openPosition, closePosition, timer / closeTime);

        if (timer >= closeTime)
            CloseOver();
    }

    protected override void OnOpenAbs()
    {
        timer = 0;
        if (myCanvas != null)
        {
            myCanvas.alpha = 1;
            myCanvas.blocksRaycasts = true;
            myCanvas.interactable = true;
        }
    }

    protected override void OnOpeningAbs(float deltaTime)
    {
        timer += deltaTime;
        transform.localPosition = Vector3.Lerp(closePosition, openPosition, timer / openTime);

        if (timer >= openTime)
            OpenOver();
    }

    protected override void OnOpenOverAbs()
    {
        transform.localPosition = openPosition;
        if (myCanvas != null)
        {
            myCanvas.alpha = 1;
            myCanvas.blocksRaycasts = true;
            myCanvas.interactable = true;
        }
    }

    protected override void Initialize()
    {
        openPosition = transform.localPosition;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        closePosition = openPosition + new Vector3(X * screenWidth, Y * screenHeight, 0);
    }
}
