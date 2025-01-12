using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetMainButton : MonoSingleton<SetMainButton>
{
    public GameObject lastInteract;
    public bool keyboard = true;

    public bool blockInput;

    public bool canButtonInteract = true;

    protected override void OnAwake()
    {
    }

    public void SetButtonInteract(GameObject interact)
    {
        lastInteract = interact;
        if (!canButtonInteract) return;
        if (!keyboard) EventSystem.current.SetSelectedGameObject(interact);
        else
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void DeselectInteract()
    {
        EventSystem.current.SetSelectedGameObject(null);
        lastInteract = null;
    }




    private void Start()
    {
        if (InputSwitcher.instance.isJoystick) keyboard = false;
        else keyboard = true;
        InputSwitcher.instance.OnChangeJoystick += OnJoystick;
        InputSwitcher.instance.OnChangeKeyboard += OnKeyboard;
        lastInteract = EventSystem.current.firstSelectedGameObject;
        InputSwitcher.instance.AddToAction("Close", CloseScreen);
        //SceneLoader.Instance.OnStartLoadScene += DeleteReferences;
    }

    void DeleteReferences()
    {
        InputSwitcher.instance.RemoveToAction("Close", CloseScreen);
        InputSwitcher.instance.OnChangeJoystick -= OnJoystick;
        InputSwitcher.instance.OnChangeKeyboard -= OnKeyboard;
        SceneLoader.Instance.OnStartLoadScene -= DeleteReferences;
    }

    void OnJoystick()
    {
        if (canButtonInteract) EventSystem.current.SetSelectedGameObject(lastInteract);
        
        keyboard = false;
    }

    void OnKeyboard()
    {
        EventSystem.current.SetSelectedGameObject(null);
        keyboard = true;
    }

    private void Update()
    {
        if (screens.Count > 0)
        {
            if(!screens[screens.Count - 1].panel.IsOpen)
            {
                screens.RemoveAt(screens.Count - 1);
                return;
            }

            if(!screens[screens.Count - 1].frameWaited)
            {
                screens[screens.Count - 1] = new Screen() { panel = screens[screens.Count - 1].panel, closeAction = screens[screens.Count - 1].closeAction, frameWaited = true };
                return;
            }
        }
    }

    public void CloseScreen(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (blockInput) return;
        if (screens.Count > 0)
        {
            if (!screens[screens.Count - 1].frameWaited)
            {
                screens[screens.Count - 1] = new Screen() { panel = screens[screens.Count - 1].panel, closeAction = screens[screens.Count - 1].closeAction, frameWaited = true };
                return;
            }

            screens[screens.Count - 1].closeAction?.Invoke();
            screens.RemoveAt(screens.Count - 1);
        }
    }

    List<Screen> screens = new List<Screen>();

    public void PushScreen(UIPanelTransition panel, System.Action newScreen)
    {
        int index = -1;

        for (int i = 0; i < screens.Count; i++)
        {
            if(panel == screens[i].panel)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            screens.Add(new Screen() { panel = panel, closeAction = newScreen });
        }
    }

    public bool IsTheLastPanel(UIPanelTransition panel)
    {
        if (screens.Count <= 0 || screens[screens.Count - 1].panel != panel)
            return false;
        else
            return true;
    }

    public struct Screen
    {
        public UIPanelTransition panel;
        public System.Action closeAction;
        public bool frameWaited;
    }
}