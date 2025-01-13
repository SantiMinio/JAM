using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerManager : MonoSingleton<TimerManager>, IPause
{
    CDModule cdModule = new CDModule();
    float currentTimer;


    protected override void OnAwake()
    {
    }

    private void Start()
    {
        PauseManager.instance?.AddToPause(this);
    }

    public string AddTimer(float timer, Action OnStartTimer, Action OnEndTimer)
    {
        string timerKey = currentTimer.ToString();
        OnStartTimer?.Invoke();
        cdModule.AddCD(timerKey, OnEndTimer, timer);
        currentTimer += 0.01f;

        return timerKey;
    }

    public void StopTimer(string key, bool executeEnd)
    {
        if (!executeEnd) cdModule.EndCDWithoutExecute(key);
        else cdModule.EndCDWithExecute(key);
    }

    public string AddAnim(float timer, Action OnStartTimer, Action OnEndTimer, Action<float> AnimCallback)
    {
        string timerKey = currentTimer.ToString();
        OnStartTimer?.Invoke();
        cdModule.AddCD(timerKey, OnEndTimer, timer, AnimCallback);
        currentTimer += 0.01f;

        return timerKey;
    }

    public string AddAOTEffect(int ticks, float ticksInterval, Action OnStartTimer, Action OnEndTimer, Action TickEffect, bool startTicking = false)
    {
        string timerKey = currentTimer.ToString();
        OnStartTimer?.Invoke();
        float interval = ticksInterval;
        int curTicks = ticks - (startTicking ? 1 : 0);
        if (startTicking)
            TickEffect?.Invoke();

        Action<float> anim = (x) =>
        {
            if (x >= interval && curTicks > 0)
            {
                interval += ticksInterval;
                TickEffect?.Invoke();
                curTicks -= 1;
            }
        };

        cdModule.AddCD(timerKey, OnEndTimer, ((float)ticks * ticksInterval) +0.1f - (startTicking ? ticksInterval : 0), anim);
        currentTimer += 0.01f;

        return timerKey;
    }

    private void Update()
    {
        if (paused) return;

        cdModule.UpdateCD();
    }

    bool paused;
    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }
}
