using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSystem
{
    public System.Action<float> TakeDmg; // stats
    public System.Action UIEvent; // 
    //public event Action playSound

    public void DmgAction(float dmg)
    {
        TakeDmg.Invoke(dmg);
    }

    public void UpdateUI()
    {
        UIEvent.Invoke();
    }
}
