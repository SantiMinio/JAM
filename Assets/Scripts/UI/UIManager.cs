using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    [SerializeField] Image lifeBar = null;
    [SerializeField] Image lifeBarTwo = null;
    [SerializeField] Image fadeImg = null;

    private void Awake()
    {
        instance = this;
    }

    public void SetLife(float lifePercent)
    {
        lifeBar.fillAmount = lifePercent;
        lifeBarTwo.fillAmount = lifePercent;
    }

    public void DoFadeIn(float duration, Action onEndFade)
    {
        TimerManager.Instance.AddAnimUnpaused(duration, () => { }, onEndFade, (x) => { fadeImg.color = new Color(0, 0, 0, x / duration); });
    }

    public void DoFadeIn(float duration, Action onEndFade, Color fadeColor)
    {
        TimerManager.Instance.AddAnimUnpaused(duration, () => { }, onEndFade, (x) => { fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, x / duration); });
    }

    public void DoFadeOut(float duration, Action onEndFade)
    {
        TimerManager.Instance.AddAnimUnpaused(duration, () => { }, onEndFade, (x) => { fadeImg.color = new Color(0, 0, 0, duration - x / duration); });
    }

    public void DoFadeOut(float duration, Action onEndFade, Color fadeColor)
    {
        TimerManager.Instance.AddAnimUnpaused(duration, () => { }, onEndFade, (x) => { fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, duration - x / duration); });
    }
}
