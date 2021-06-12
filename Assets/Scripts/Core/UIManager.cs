using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    [SerializeField] Image lifeBar = null;

    private void Awake()
    {
        instance = this;
    }

    public void SetLife(float lifePercent)
    {
        lifeBar.fillAmount = lifePercent;
    }
}
