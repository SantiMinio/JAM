using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PageSelector<T> : MonoBehaviour
{
    [SerializeField] protected T[] myPanels = new T[0];

    [SerializeField] protected UIPanelTransition panel = null;

    [SerializeField] bool carrousel = true;
    [SerializeField] UnityEvent<int> OnChangePage = new UnityEvent<int>();
    protected int currentIndex = -1;

    [HideInInspector] public bool canChangePage = true;

    public void Open()
    {
        InputSwitcher.instance.AddToAction("ChangePageLeft", SetToPrevPage);
        InputSwitcher.instance.AddToAction("ChangePageRight", SetToNextPage);
        panel.Open();
        SetCurrentPanel(0);
        SetMainButton.Instance.PushScreen(panel, Close);
        OnOpen();
    }

    private void OnDestroy()
    {
        if (!panel.IsOpen) return;
        InputSwitcher.instance.RemoveToAction("ChangePageLeft", SetToPrevPage);
        InputSwitcher.instance.RemoveToAction("ChangePageRight", SetToNextPage);
    }

    public void Close()
    {
        if (SetMainButton.Instance != null && SetMainButton.Instance.blockInput) return;
        if (!panel.IsOpen) return;
        InputSwitcher.instance.RemoveToAction("ChangePageLeft", SetToPrevPage);
        InputSwitcher.instance.RemoveToAction("ChangePageRight", SetToNextPage);
        if (currentIndex > -1)
            OnClose();

        currentIndex = -1;

        panel.Close();
    }

    protected abstract void OnOpen();

    protected abstract void OnClose();

    protected virtual void Update()
    {
    }

    public void GoToNextPage()
    {
        if (canChangePage)
            SetCurrentPanel(NextPage());
    }

    public void GoToPrevPage()
    {
        if (canChangePage)
            SetCurrentPanel(PrevPage());
    }

    public void SetCurrentPanel(int index)
    {
        if (!canChangePage) return;

        if (currentIndex == index) return;

        var lastIndex = currentIndex;

        currentIndex = index;

        OnSetNewPanel(lastIndex, currentIndex);

        if(lastIndex != currentIndex)
        {
            OnChangePage.Invoke(currentIndex);
        }
    }

    void SetToNextPage(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!panel.IsOpen || !SetMainButton.Instance.IsTheLastPanel(panel)) return;
        if (!carrousel && currentIndex >= myPanels.Length - 1) return;

        GoToNextPage();
    }

    void SetToPrevPage(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!panel.IsOpen || !SetMainButton.Instance.IsTheLastPanel(panel)) return;
        if (!carrousel && currentIndex <= 0) return;

        GoToPrevPage();
    }

    protected abstract void OnSetNewPanel(int lastIndex, int _currentIndex);

    protected int PrevPage()
    {
        return currentIndex - 1 < 0 ? myPanels.Length - 1 : currentIndex - 1;
    }

    protected int NextPage()
    {
        return currentIndex + 1 >= myPanels.Length ? 0 : currentIndex + 1;
    }

    public void SetButton(GameObject go)
    {
        SetMainButton.Instance.SetButtonInteract(go);
    }
}
