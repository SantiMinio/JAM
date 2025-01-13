using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageSelectorUI : IndexSelectorUI<string>
{
    [SerializeField] TextMeshProUGUI text = null;

    [SerializeField] GameObject lowerArrowSelector = null;
    [SerializeField] GameObject higherArrowSelector = null;
    [SerializeField] string[] qualitys = new string[0];

    [SerializeField] SetMainButton main = null;

    private void Awake()
    {
        OnReachMaxIndexEvent += ImageSelectorUI_OnReachMaxIndexEvent;
        OnReachMinIndexEvent += ImageSelectorUI_OnReachMinIndexEvent;
    }

    private void ImageSelectorUI_OnReachMaxIndexEvent()
    {
        higherArrowSelector.SetActive(false);
        main.SetButtonInteract(lowerArrowSelector);
    }

    private void ImageSelectorUI_OnReachMinIndexEvent()
    {
        lowerArrowSelector.SetActive(false);
        main.SetButtonInteract(higherArrowSelector);
    }

    protected override void OnChangeIndexAbs(int newIndex, int oldIndex)
    {
        text.text = qualitys[newIndex];
        lowerArrowSelector.SetActive(true);
        higherArrowSelector.SetActive(true);
    }

    public override void InitializeIndexer(string[] elements, int firstIndex)
    {
        qualitys = elements;
        index = firstIndex;
        SetMaxIndex(qualitys.Length - 1);
        text.text = qualitys[index];
        if (index <= minIndex)
            lowerArrowSelector.SetActive(false);
        else if (index >= maxIndex)
            higherArrowSelector.SetActive(false);
    }
}
