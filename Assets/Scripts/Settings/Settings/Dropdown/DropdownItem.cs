using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownItem : MonoBehaviour
{
    System.Action<int> OnClickEvent;
    public UnityEngine.UI.Button button;
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] GameObject checkMark = null;

    public int Index { get; private set; }

    public string Text { get => text.text; }

    public void Initialize(string txt, bool isSelected, int _index, System.Action<int> _OnClickEvent)
    {
        text.text = txt;
        OnClickEvent = _OnClickEvent;
        if (isSelected)
            SetSelected();
        else
            UnSelect();
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClick));
        Index = _index;
    }

    public void OnClick()
    {
        OnClickEvent?.Invoke(Index);
    }


    public void SetSelected()
    {
        checkMark.SetActive(true);
    }

    public void UnSelect()
    {
        checkMark.SetActive(false);
    }
}
