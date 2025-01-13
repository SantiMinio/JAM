using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class CustomDropdown : MonoBehaviour
{
    [SerializeField] bool initializeOnAwake = false;
    [SerializeField] string[] itemsToInitialize = new string[0];

    [SerializeField] DropdownItem item = null;
    [SerializeField] Transform itemParent = null;
    [SerializeField] UIPanelTransition dropdownItems = null;
    [SerializeField] TextMeshProUGUI mainText = null;

    public UnityEvent<int> OnSelectItem = new UnityEvent<int>();

    DropdownItem[] items = new DropdownItem[0];

    public int ItemsAmount { get => items.Length; }

    bool Opened = false;

    public int CurrentSelectedItem { get; private set; }

    public event Action OnOpenDropdown;

    private void Awake()
    {
        if (initializeOnAwake) SetItems(itemsToInitialize, 0);
    }


    public void SetItems(string[] dropdownItems, int selectedItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].gameObject);
        }

        items = new DropdownItem[dropdownItems.Length];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Instantiate(item, itemParent);
            items[i].gameObject.SetActive(true);
            items[i].Initialize(dropdownItems[i], i == selectedItem, i, SelectItem);

            Navigation newNav = new Navigation();

            newNav.mode = Navigation.Mode.Explicit;
            newNav.selectOnUp = i == 0 ? null : items[i - 1].button;

            items[i].button.navigation = newNav;

            if(i > 0)
            {
                Navigation newNavLastItem = items[i - 1].button.navigation;

                newNavLastItem.selectOnDown = items[i].button;

                items[i - 1].button.navigation = newNavLastItem;
            }
        }

        SetMainText(dropdownItems[selectedItem]);

        CurrentSelectedItem = selectedItem;
    }

    public void Open()
    {
        dropdownItems.Open();
        OnOpenDropdown?.Invoke();
        SetMainButton.Instance.PushScreen(dropdownItems, Close);
        Opened = true;
        if(items.Length > 0)SetMainButton.Instance.SetButtonInteract(items[0].gameObject);
    }

    public void Close()
    {
        OnClose();

    }

    void OnClose()
    {
        dropdownItems.Close();
        Opened = false;
        SetMainButton.Instance.SetButtonInteract(gameObject);
    }

    public void SetButton(GameObject go)
    {
        SetMainButton.Instance.SetButtonInteract(go);
    }

    public void SetIndex(int index) 
    {
        items[CurrentSelectedItem].UnSelect();

        SetMainText(items[index].Text);

        CurrentSelectedItem = index;

        items[CurrentSelectedItem].SetSelected();
    }

    public void SelectItem(int index)
    {
        
        items[CurrentSelectedItem].UnSelect();

        SetMainText(items[index].Text);

        CurrentSelectedItem = index;

        items[CurrentSelectedItem].SetSelected();
        Close();

        OnSelectItem.Invoke(index);
    }

    void SetMainText(string text)
    {
        mainText.text = text;
    }

}
