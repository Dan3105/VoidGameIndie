using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : SingletonTemplate<UIManager>
{
    public Image hpBar;
    public Image expBar;

    public void UpdateBar(float amount, Image bar)
    {
        bar.fillAmount = amount;
    }

    public float CalPercentBar(float current, float max)
    {
        return current / max;
    }
}
