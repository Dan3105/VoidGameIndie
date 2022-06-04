using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Data", fileName = "Create Weapon Stats")]
public class WeaponStats : ScriptableObject {
    public float dmgAtk;
    public float cdAtk;
    public float rangeDetect;

    public float currentDmg;
    public float currentCD;

    public void SetStats()
    {
        currentDmg = dmgAtk;
        currentCD = cdAtk;
    }
    public void UpdateStats(int level)
    {
        currentDmg = dmgAtk * (1 + level) / 2;
      
    }

    public void SetDmg(float percent)
    {
        currentDmg += percent * currentDmg;
    }

    public void SetCD(float percent)
    {
        currentCD -= percent * currentCD;
    }
}
