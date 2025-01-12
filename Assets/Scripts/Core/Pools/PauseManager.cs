using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance { get; private set; }

    public event Action OnStopGame;
    List<IPause> pauseList = new List<IPause>();

    [SerializeField] UnityEvent OnPauseGame = new UnityEvent();
    [SerializeField] UnityEvent OnResumeGame = new UnityEvent();

    [SerializeField] UIPanelTransition pausePanel = null;

    public bool paused;
    bool canResume;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InputSwitcher.instance.AddToAction("PauseInput", GetPauseInput);
    }

    private void OnDestroy()
    {
        InputSwitcher.instance.RemoveToAction("PauseInput", GetPauseInput);
    }

    public void AddToPause(IPause pauseEntity)
    {
        if (!pauseList.Contains(pauseEntity)) pauseList.Add(pauseEntity);
    }

    public void RemoveToPause(IPause pauseEntity)
    {
        if (pauseList.Contains(pauseEntity)) pauseList.Remove(pauseEntity);
    }

    void GetPauseInput(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        PauseMenuUI();
    }

    public void SwitchPause()
    {
        if (paused)
        {
            ResumeMenuUI();
        }
        else
        {
            PauseMenuUI();
        }
    }

    public void PauseGame(bool pauseParticlesAndSounds = true, bool pauseOnlyLoop = false)
    {
        if (paused) return;
        for (int i = 0; i < pauseList.Count; i++)
        {
            pauseList[i].Pause();
        }
        if (pauseParticlesAndSounds)
        {
            ParticlesManager.Instance.PauseParticles(pauseOnlyLoop);
            AudioManager.Instance.PauseSounds();
        }
        paused = true;
        //StartCoroutine(Wait());
    }

    public void ResumeGame(bool pauseParticlesAndSounds = true)
    {
        if (!paused) return;
        for (int i = 0; i < pauseList.Count; i++)
        {
            pauseList[i].Resume();
        }

        if (pauseParticlesAndSounds)
        {
            ParticlesManager.Instance.ResumeParticles();
            AudioManager.Instance.ResumeSounds();
        }
        paused = false;
        canResume = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        canResume = true;
    }

    public void PauseMenuUI()
    {
        pausePanel.Open();
        SetMainButton.Instance.PushScreen(pausePanel, ResumeMenuUI);
        PauseGame(true);
        OnPauseGame?.Invoke();
        StartCoroutine(Wait());
    }

    public void ResumeMenuUI()
    {
        pausePanel.Close();
        ResumeGame(true);
        OnResumeGame?.Invoke();
    }

    public void StopGame()
    {
        OnStopGame?.Invoke();
    }
}
